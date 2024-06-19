import { createMemoryHistory, createRouter } from "vue-router";
import home from "./components/pages/home.vue";
import dashboard from "./components/auth/Dashboard.vue";
import cart from "./components/Cart/cart.vue";
import products from "./components/pages/products.vue";
import productDetail from "./components/Product/productDetail.vue";
import Blog from "./components/pages/blogs.vue";
import About from "./components/pages/about.vue";
import Contact from "./components/pages/contact.vue";
import checkOutPage from "./components/checkOut/checkOutPage.vue";
import Success from "./components/checkOut/Success.vue";
import userDetail from "./components/user/userDetail.vue";
import account from "./components/user/Account.vue";
import order from "./components/user/OderList.vue";
import help from "./components/user/help.vue";
const routes = [
  { path: "/", component: home },
  { path: "/SignIn", component: dashboard },
  { path: "/Cart", component: cart },
  { path: "/Products", component: products },
  {
    path: "/Products/:ProductName",
    name: "ProductDetail",
    component: productDetail,
    props: true,
  },
  { path: "/Blog", component: Blog },
  { path: "/About", component: About },
  { path: "/Contact", component: Contact },
  { path: "/Order", component: checkOutPage },
  { path: "/success", component: Success },
  {
    path: "/userDetail",
    component: userDetail,
    children: [
      { path: "account", component: account },
      { path: "order", component: order },
      { path: "help", component: help },
    ],
  },
];

const router = createRouter({
  history: createMemoryHistory(),
  routes,
});

export default router;
