<template>
    <div class=" banner h-[550px] w-full relative mb-32">
        <img :src="imgUrl" alt="" class=" h-full w-full">
        <div class="title absolute top-0 left-0 w-full h-full flex justify-center items-center"
            style=" background-color: rgba(36, 36, 36, 0.67)">
            <div class=" text-center w-[80%] text-white">
                <h1 class=" text-5xl">About Us</h1>
                <p class=" mt-5">{{ title }}</p>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            imgUrl: '../src/assets/bannerAbout.jpg',
            title: `"Xin chào và chào mừng đến với Localhost
         - Nơi tìm kiếm sự tươi ngon và giàu dinh dưỡng.
          Tại đây, chúng tôi không chỉ cung cấp những trái cây tươi ngon nhất mà còn mang lại cho bạn một trải nghiệm mua sắm thú vị và an tâm.
           Với cam kết đồng hành cùng khách hàng trong hành trình chăm sóc sức khỏe và hạnh phúc, 
           chúng tôi tự hào về sứ mệnh mang đến những sản phẩm chất lượng và dịch vụ tận tâm nhất.
            Hãy cùng chúng tôi khám phá vẻ đẹp tự nhiên và lối sống lành mạnh thông qua những trái cây tươi ngon nhất từ Localhost!"`
        }
    },
}
</script>

<style></style>