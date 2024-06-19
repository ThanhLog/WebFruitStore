<script setup>
import { RouterLink, RouterView } from 'vue-router'
import menuVue from './components/menu.vue'
</script>

<template>
  <div class=" h-screen flex">
    <menuVue class="w-1/5"/>
    <RouterView class=" w-4/5 h-full overflow-x-auto"/>
  </div>
</template>

<style scoped>

</style>
