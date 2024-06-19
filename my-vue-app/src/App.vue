<script setup>
import { RouterLink, RouterView } from 'vue-router'
import footerVue from './components/footer.vue';
import menuVue from "./components/menu.vue";
</script>

<template>
  <div>
    <menuVue />
    <RouterView />
    <footerVue />
  </div>
</template>

<style scoped>
</style>
