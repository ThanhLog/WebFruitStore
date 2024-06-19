<template>
  <div class=" mt-16 w-full">
    <div class="Introduce w-full flex items-center">
      <div class=" w-[60%] z-10">
        <h1 class=" text-5xl text-center">Giới thiệu</h1>
        <p class=" px-20 mt-5">Lorem ipsum dolor sit, amet consectetur adipisicing elit. Natus explicabo eveniet veniam quasi nisi delectus
          blanditiis repudiandae exercitationem! Quaerat blanditiis voluptatem, repudiandae velit qui, voluptas nostrum
          provident expedita autem odio iure consequatur vero! Dolorem temporibus minima non facilis qui! Atque fugiat
          beatae, labore tempora placeat distinctio, id ex unde quos illum recusandae numquam quo. At fugit laborum
          aperiam? Ab ratione facere architecto veritatis totam assumenda nostrum facilis optio dicta, ullam provident
          molestias quaerat natus vero harum dolore repudiandae alias ipsum quod nesciunt eligendi id dolorum! Modi
          voluptas voluptatibus quidem iusto sit, similique fugiat suscipit ducimus ipsum, placeat molestias corporis
          incidunt!</p>
      </div>
      <div class="imgIntroduce h-full relative w-[40%]">
        <img :src="Introduce1" alt="" class=" absolute bottom-[0%] -left-20 opacity-90 w-[70%]"/>
        <img :src="Introduce2" alt=""  class=" absolute top-[-10%] -left-48 w-[70%]"/>
        <img :src="backgroundIntroduce" alt="" class=" w-full">
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Introduce',
  data() {
    return {
      Introduce1: '../src/assets/banner-infor-index-1.png',
      Introduce2: '../src/assets/banner-infor-index-2.webp',
      backgroundIntroduce: '../src/assets/background-infor-index.png'
    }
  },
}
</script>

<style>
</style>