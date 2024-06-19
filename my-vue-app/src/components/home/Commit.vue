<template>
    <div class=" w-full block py-10 flex items-center justify-between ">
        <img :src="imgBanner" alt="" class="">
        <div class="content">
            <h1 class=" text-3xl font-medium">Chúng tôi tin tưởng vào việc làm của những nông dân được công nhận</h1>
            <p class=" mt-5 font-normal">Đơn giản là văn bản giả của ngành in ấn và sắp chữ. Lorem đã không còn là văn bản giả tiêu chuẩn của
                ngành kể từ những năm 1500, khi một nhà in vô danh sử dụng một chiếc bếp.</p>
            <div class=" w-full mt-5">
                <div class=" flex items-center gap-5 w-[80%] mt-2">
                    <div class=" border-2 border-[#111] bg-[#FFC94A] w-fit h-fit rounded-full p-2 ml-5 my-3">
                        <img :src="diamondIcon" alt="">
                    </div>
                    <div>
                        <h2 class=" font-semibold text-lg">Chỉ thực phẩm hữu cơ</h2>
                        <p>Lorem ipsum dolor sit amet consectetur adipisicing elit. Enim quia ab aperiam odio, delectus
                            consequatur.</p>
                    </div>
                </div>
                <div class=" flex items-center gap-5 w-[80%] mt-2">
                    <div class=" border-2 border-[#111] bg-[#FFC94A] w-fit h-fit rounded-full p-2 ml-5 my-3">
                        <img :src="diamondIcon" alt="">
                    </div>
                    <div>
                        <h2 class=" font-semibold text-lg">Tiêu chuẩn chất lượng</h2>
                        <p>Lorem ipsum dolor sit, amet consectetur adipisicing elit. Dolor suscipit, pariatur unde
                            delectus illo sed!</p>
                    </div>

                </div>
            </div>
            <div class=" mt-10 ml-5">
                <router-link to="/Products" class=" rounded-full text-white font-semibold p-3 bg-slate-800">Đặt hàng ngay</router-link>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Commit',
    data() {
        return {
            imgBanner: '../src/assets/cung-hoang-dao-2.png',
            diamondIcon: '../src/assets/icons/diamond_3914401.svg',
        }
    },
}
</script>

<style></style>