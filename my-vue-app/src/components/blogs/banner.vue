<template>
    <div class=" w-full h-[550px] relative">
        <img :src="imgBanner" alt="" class=" h-full w-full object-cover">
        <div class=" absolute w-full h-full top-0 left-0 flex justify-center items-center z-20"
            style=" background: #7b7b7b3d">
            <h1 class=" text-5xl text-white font-semibold">Localhost <span class=" font-extrabold">Blog</span></h1>
        </div>
    </div>
</template>

<script>
export default {
    name: 'banner',
    data() {
        return {
            imgBanner:'../src/assets/bannerBlog.jpg'
        }
    },
}
</script>

<style>
</style>