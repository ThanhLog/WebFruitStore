import { createRouter, createWebHistory } from "vue-router";

import accountView from "../views/accountView.vue";
import homePageView from "@/views/homePageView.vue";
import productView from "@/views/productView.vue";
import orderView from "@/views/orderView.vue";
import messengerView from "@/views/messengerView.vue";
import ProductForm from "@/components/Product/ProductForm.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    { path: "/Trang-chu", component: homePageView },
    { path: "/Quan-ly-nguoi-dung", component: accountView },
    { path: "/Quan-ly-san-pham", component: productView },
    { path: "/Quan-ly-don-hang", component: orderView },
    { path: "/Tin-nhan", component: messengerView },
    { path: "/Tao-san-pham", component: ProductForm },
  ],
});

export default router;
