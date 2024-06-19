import { createStore } from "vuex";
import axios from "axios";
import * as signalR from "@microsoft/signalr";

const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5022/cartHub")
  .build();

connection.start().catch((err) => console.error(err.toString()));

const store = createStore({
  state() {
    return {
      isLogIn: false,
      user: null,
      userName: null,
      userInfo: [],
      Products: [],
      TopSell: [],
      Cart: [],
    };
  },
  mutations: {
    SET_USER_INFO(state, userInfo) {
      if (Array.isArray(userInfo) && userInfo.length > 0) {
        state.userInfo = userInfo;
        state.userName = userInfo[0].UserName;
        localStorage.setItem("userInfo", JSON.stringify(userInfo));
      } else {
        state.userInfo = [];
        state.userName = null;
        localStorage.removeItem("userInfo");
      }
    },

    SET_LOGIN(state, userInfo) {
      state.isLogIn = true;
      state.userInfo = userInfo;
      state.userName = userInfo.length > 0 ? userInfo[0].UserName : null;
      localStorage.setItem("userInfo", JSON.stringify(userInfo));
    },
    SET_LOGOUT(state) {
      state.isLogIn = false;
      state.user = null;
      state.userName = null;
      state.userInfo = [];
      localStorage.removeItem("userInfo");
    },
    SET_CART(state, CartData) {
      state.Cart = CartData;
    },
    SET_TOP_SELL(state, products) {
      state.TopSell = products;
    },
    SET_PRODUCTS(state, products) {
      state.Products = products;
    },
    REMOVE_FROM_CART(state, { productName, userName }) {
      state.Cart = state.Cart.filter(
        (item) => item.ProductName !== productName || item.UserName !== userName
      );
    },
    UPDATE_CART_ITEM(state, updatedItem) {
      const index = state.Cart.findIndex(
        (item) =>
          item.ProductName === updatedItem.ProductName &&
          item.UserName === updatedItem.UserName
      );
      if (index !== -1) {
        state.Cart.splice(index, 1, updatedItem);
      } else {
        state.Cart.push(updatedItem);
      }
    },

    UPDATE_CART_ITEM_REALTIME(state, { productName, quantity }) {
      const index = state.Cart.findIndex(
        (item) =>
          item.ProductName === productName && item.UserName === state.userName
      );
      if (index !== -1) {
        state.Cart[index].Quantity = quantity;
      }
    },
  },
  actions: {
    async signIn({ commit }, { UserName, password }) {
      try {
        const response = await axios.post(
          "http://localhost:5022/api/Accounts/check",
          {
            userName: UserName,
            password: password,
          },
          {
            headers: { "Content-Type": "application/json" },
          }
        );

        const userInfo = response.data;
        commit("SET_LOGIN", userInfo);
        await this.dispatch("fetchUserInfo", UserName);
      } catch (error) {
        console.error(error);
        throw error;
      }
    },
    async fetchUserInfo({ commit }, UserName) {
      try {
        const response = await axios.get(
          `http://localhost:5022/api/Accounts/${UserName}`
        );
        const userInfo = response.data;
        commit("SET_USER_INFO", userInfo);
      } catch (error) {
        console.error(error);
      }
    },
    async fetchDataCartUser({ commit }, UserName) {
      try {
        if (!UserName) throw new Error("Missing UserName");

        const response = await axios.get(
          `http://localhost:5022/api/Carts/${UserName}`
        );
        const cartData = response.data;
        if (!Array.isArray(cartData))
          throw new Error("Invalid cart data format");

        commit("SET_CART", cartData);
      } catch (error) {
        console.error("Lỗi khi lấy dữ liệu giỏ hàng:", error);
      }
    },

    async updateProductQuantity(
      { commit },
      { UserName, ProductName, Quantity }
    ) {
      try {
        const payload = { UserName, ProductName, Quantity };
        const url = `http://localhost:5022/api/Carts/UpdateCartQuantity?UserName=${encodeURIComponent(
          UserName
        )}&ProductName=${encodeURIComponent(ProductName)}&Quantity=${Quantity}`;
        const response = await axios.put(
          url,
          {},
          {
            headers: { "Content-Type": "application/json" },
          }
        );

        const updatedItem = response.data;
        console.log("Update cart quantity response:", updatedItem);
        commit("UPDATE_CART_ITEM", updatedItem);
      } catch (error) {
        console.error("Failed to update cart quantity:", error);
        if (error.response) {
          console.error("Error response data:", error.response.data);
          console.error("Error response status:", error.response.status);
          console.error("Error response headers:", error.response.headers);
        } else if (error.request) {
          console.error("Error request:", error.request);
        } else {
          console.error("Error message:", error.message);
        }
        throw error;
      }
    },

    async removeItemFromCart({ commit }, { productName, UserName }) {
      try {
        //  trong trường hợp này, encodeURIComponent được sử dụng để đảm bảo rằng dữ liệu truyền vào URL không gây ra lỗi và giữ nguyên ý nghĩa của nó.
        const url = `http://localhost:5022/api/Carts/RemoveCart?UserName=${encodeURIComponent(
          UserName
        )}&ProductName=${encodeURIComponent(productName)}`;
        const response = await axios.delete(url);

        console.log("Item removed from cart:", response.data);
        commit("REMOVE_FROM_CART", { productName, userName: UserName });
      } catch (error) {
        console.error("Error removing item from cart:", error);
        throw error;
      }
    },

    async clearCart({ commit }, UserName) {
      try {
        if (!UserName) throw new Error("Missing UserName");

        const response = await axios.delete(
          `http://localhost:5022/api/Carts/ClearCart/${UserName}`
        );
        console.log(
          "Tất cả sản phẩm đã được xóa khỏi giỏ hàng:",
          response.data
        );

        // Gọi mutation để cập nhật state trong store
        commit("SET_CART", []);
      } catch (error) {
        console.error("Error clearing cart:", error);
      }
    },

    async fetchTopSellProduct({ commit }) {
      try {
        const response = await axios.get(
          "http://localhost:5022/api/Products/TopSell"
        );
        const products = response.data;
        commit("SET_TOP_SELL", products);
      } catch (error) {
        console.error("Error fetching top sell products:", error);
      }
    },
    async fetchProducts({ commit }) {
      try {
        const response = await axios.get("http://localhost:5022/api/Products");
        const products = response.data;
        commit("SET_PRODUCTS", products);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    },

    async createOrder({ commit, state }, orderData) {
      try {
        // Lấy thông tin UserName từ state.userInfo
        const userName =
          state.userInfo.length > 0 ? state.userInfo[0].UserName : null;
        if (!userName) {
          console.error("User information not available");
          return; // Thoát nếu không có thông tin người dùng
        }

        // Thêm UserName vào đối tượng orderData
        orderData.userName = userName;

        // Gửi yêu cầu tạo đơn hàng đến API
        const response = await axios.post(
          "http://localhost:5022/api/Orders",
          orderData
        );
        const createdOrder = response.data;
        console.log("Created order:", createdOrder);

        // Sau khi đơn hàng được tạo thành công, xóa giỏ hàng
        await this.dispatch("clearCart", userName);

        // Thực hiện các thao tác cần thiết sau khi tạo đơn hàng thành công
      } catch (error) {
        console.error("Error creating order:", error);
        // Xử lý lỗi, ví dụ: hiển thị thông báo lỗi cho người dùng
      }
    },
  },
  getters: {
    isLogIn(state) {
      return state.isLogIn;
    },
    userInfo(state) {
      return state.userInfo;
    },
    userName(state) {
      return state.userInfo.length > 0 ? state.userInfo[0].UserName : null;
    },
    dataCartUser(state) {
      return state.Cart;
    },
    topsell(state) {
      return state.TopSell;
    },
    allProduct(state) {
      return state.Products;
    },
  },
});

connection.on("CartUpdated", () => {
  store.dispatch("fetchCartData"); // Tạo action fetchCartData để lấy dữ liệu giỏ hàng mới nhất
});

export default store;
