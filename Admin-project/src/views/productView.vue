<template>
  <div class=" relative">
    <div class=" sticky top-0 left-0 bg-white flex items-center justify-between px-5">
      <h2 class="p-5 text-2xl">Product List</h2>
      <ul class="flex gap-2 items-center">
        <li class="flex items-center px-3 py-1 border-2 rounded-full">
          <input v-model="searchTerm" type="text" placeholder="Tìm kiếm sản phẩm" class="px-3 py-2 outline-none">
          <button @click="searchProducts" class="btnSearch">
            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 512 512">
              <path fill="currentColor"
                d="M464 428L339.92 303.9a160.48 160.48 0 0 0 30.72-94.58C370.64 120.37 298.27 48 209.32 48S48 120.37 48 209.32s72.37 161.32 161.32 161.32a160.48 160.48 0 0 0 94.58-30.72L428 464ZM209.32 319.69a110.38 110.38 0 1 1 110.37-110.37a110.5 110.5 0 0 1-110.37 110.37" />
            </svg>
          </button>
        </li>
        <li>
          <router-link to="/Tao-san-pham"
            class=" flex gap-1 font-semibold bg-green-600 text-white px-3 py-2  rounded-xl">
            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 24 24">
              <path fill="currentColor"
                d="M12 2C6.477 2 2 6.477 2 12s4.477 10 10 10s10-4.477 10-10S17.523 2 12 2m5 11h-4v4h-2v-4H7v-2h4V7h2v4h4z" />
            </svg>ADD
          </router-link>
        </li>
        <li>
          <button @click="openFormDel" class="flex gap-1 font-semibold bg-red-600 text-white px-3 py-2 rounded-xl">
            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 24 24">
              <g fill="none">
                <path
                  d="M24 0v24H0V0zM12.593 23.258l-.011.002l-.071.035l-.02.004l-.014-.004l-.071-.035c-.01-.004-.019-.001-.024.005l-.004.01l-.017.428l.005.02l.01.013l.104.074l.015.004l.012-.004l.104-.074l.012-.016l.004-.017l-.017-.427c-.002-.01-.009-.017-.017-.018m.265-.113l-.013.002l-.185.093l-.01.01l-.003.011l.018.43l.005.012l.008.007l.201.093c.012.004.023 0 .029-.008l.004-.014l-.034-.614c-.003-.012-.01-.02-.02-.022m-.715.002a.023.023 0 0 0-.027.006l-.006.014l-.034.614c0 .012.007.02.017.024l.015-.002l.201-.093l.01-.008l.004-.011l.017-.43l-.003-.012l-.01-.01z" />
                <path fill="currentColor"
                  d="M20 5a1 1 0 1 1 0 2h-1l-.003.071l-.933 13.071A2 2 0 0 1 16.069 22H7.93a2 2 0 0 1-1.995-1.858l-.933-13.07A1.017 1.017 0 0 1 5 7H4a1 1 0 0 1 0-2zm-3.003 2H7.003l.928 13h8.138zM14 2a1 1 0 1 1 0 2h-4a1 1 0 0 1 0-2z" />
              </g>
            </svg>
            Delete
          </button>
          <div v-if="showFormDel"
            class="absolute flex right-5 p-5 bg-white shadow-2xl rounded-full bottom-[-100%] text-xl">
            <input v-model="productIdToDelete" type="text" placeholder="Id sản phẩm" class="outline-none w-[250px]">
            <div>
              <button @click="deleteProduct" class=" bg-green-600 text-white px-2 py-1 rounded-full">Xác nhận</button>
              <button @click="closeFormDel" class="ml-3 bg-red-600 text-white px-2 py-1 rounded-full">Hủy</button>
            </div>
          </div>
        </li>
      </ul>
    </div>
    <ul class="p-5 grid grid-cols-4 gap-6">
      <li v-for="(product, index) in products" :key="index" :id="'product-' + product.ProductID"
        class="border-2 w-fit p-3 flex flex-col items-center">
        <div><img :src="product.Image" alt="Product Image" style="width: 100px; height: 100px;"></div>
        <div>Product ID: {{ product.ProductID }}</div>
        <div>Product Name: {{ product.ProductName }}</div>
        <div>Origin: {{ product.Origin }}</div>
        <div>Price: {{ product.Price }}</div>
        <div>Sold: {{ product.Sold }}</div>
        <div>Stock: {{ product.Stock }}</div>
        <div>Imported: {{ product.Imported }}</div>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ProductList',
  data() {
    return {
      products: [],
      showFormDel: false,
      productIdToDelete: '',
      searchTerm: '',
      foundItemId: null // Thêm biến mới
    };
  },
  mounted() {
    this.fetchProducts();
  },
  methods: {
    async fetchProducts() {
      try {
        const response = await axios.get('http://localhost:5022/api/Products');
        this.products = response.data;
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    },
    openFormDel() {
      this.showFormDel = true;
    },
    closeFormDel() {
      this.showFormDel = false;
    },
    async deleteProduct() {
      if (!this.productIdToDelete) {
        alert('Vui lòng nhập Id sản phẩm');
        return;
      }

      try {
        const response = await axios.delete(`http://localhost:5022/api/Products/${this.productIdToDelete}`);
        console.log(response.data);
        alert('Xóa sản phẩm thành công');
        this.showFormDel = false;
        this.productIdToDelete = '';
      } catch (error) {
        console.error(error);
        alert('Xóa sản phẩm thất bại');
      }
    },
    async searchProducts() {
      try {
        const response = await axios.get(`http://localhost:5022/api/Products/Search?searchTerm=${this.searchTerm}`);
        console.log(response.data);

        // Lưu ID của mục được tìm thấy
        if (response.data.length > 0) {
          this.foundItemId = response.data[0].ProductID;
        }

        // Xử lý kết quả tìm kiếm ở đây
        // Sau khi xử lý, cuộn trang đến mục được tìm thấy
        this.scrollToFoundItem();
      } catch (error) {
        console.error(error);
        alert('Đã xảy ra lỗi khi tìm kiếm sản phẩm');
      }
    },
    scrollToFoundItem() {
      if (this.foundItemId !== null) {
        const foundItem = document.getElementById(`product-${this.foundItemId}`);
        if (foundItem) {
          foundItem.scrollIntoView({ behavior: 'smooth', block: 'start' });
        }
      }
    }
  }
};
</script>

<style>
/* Add your styles here */
</style>
