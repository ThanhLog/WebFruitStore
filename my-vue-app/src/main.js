// main.js
import { createApp } from "vue";
import App from "./App.vue";
import store from "./store";
import router from "./router"; // if you have a router
import './style.css'
const app = createApp(App);
app.use(store);
app.use(router); // if you have a router
app.mount("#app");
