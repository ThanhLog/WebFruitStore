<template>
    <div class=" mt-5 p-10 bg-white">
        <div class=" flex gap-[20px]">
            <h1 class=" text-3xl w-[60%] leading-relaxed">{{ titlle }}</h1>
            <p class=" w-[40%] leading-loose ">{{ confirm }}</p>
        </div>
        <div class=" grid grid-cols-3 gap-10 px-10 mt-10">
            <div class=" col-span-1 rounded-md p-4" style="box-shadow: 0px 10px 36px -3px rgba(0,0,0,0.1);">
                <div class=" border-2 border-[#111] bg-[#FFC94A] w-fit h-fit rounded-full p-2 ml-5 my-3">
                    <img :src="farmIcon" alt="">
                </div>
                <div class=" w-full px-5 h[150px]">
                    <h3 class=" text-2xl font-medium h-[50px]">Sở hữu vườn trái cây</h3>
                    <p class=" mt-3 h-[100px] font-extralight">Trực tiếp từ khu vườn của chính họ trải rộng khắp các tỉnh có chi nhánh cửa hàng Jez Salad.</p>
                </div>
                <button class=" border-2 px-5 py-2 rounded-full font-semibold hover:bg-[#ECEFF7]">Đọc chi tiết</button>
            </div>
            <div class=" col-span-1 rounded-md p-4" style="box-shadow: 0px 10px 36px -3px rgba(0,0,0,0.1);">
                <div class=" border-2 border-[#111] bg-[#FFC94A] w-fit h-fit rounded-full p-2 ml-5 my-3">
                    <img :src="diamondIcon" alt="">
                </div>
                <div class=" w-full px-5 h[150px]">
                    <h3 class=" text-2xl font-medium h-[50px]">Salad trái cây tốt cho sức khỏe</h3>
                    <p class=" mt-3 h-[100px] font-extralight">Tiên phong về các món salad trái cây tốt cho sức khỏe, thơm ngon & phù hợp với mọi người với chất
                        lượng đảm bảo tốt nhất.</p>
                </div>
                <button class=" border-2 px-5 py-2 rounded-full font-semibold hover:bg-[#ECEFF7]">Đọc chi tiết</button>
            </div>
            <div class=" col-span-1 rounded-md p-4" style="box-shadow: 0px 10px 36px -3px rgba(0,0,0,0.1);">
                <div class=" border-2 border-[#111] bg-[#FFC94A] w-fit h-fit rounded-full p-2 ml-5 my-3">
                    <img :src="trophyIcon" alt="">
                </div>
                <div class=" w-full px-5 h[150px]">
                    <h3 class=" text-2xl font-medium h-[50px]">100 thương hiệu hàng đầu</h3>
                    <p class=" mt-3 h-[100px] font-extralight">Chúng tôi là một trong những thương hiệu tốt nhất trong lĩnh vực Thực phẩm và Đồ uống ở
                        Indonesia.
                    </p>
                </div>
                <button class=" border-2 px-5 py-2 rounded-full font-semibold hover:bg-[#ECEFF7]">Đọc chi tiết</button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            titlle: 'TẠI SAO CHỌN CHÚNG TÔI CHO THỰC PHẨM SỨC KHỎE CỦA BẠN',
            confirm: 'Chúng tôi tiếp tục lựa chọn và duy trì chất lượng của trái cây được phục vụ một cách nhất quán để nó vẫn tươi và bổ dưỡng khi bạn ăn.',
            farmIcon: '../src/assets/icons/farm_9245142.svg',
            diamondIcon: '../src/assets/icons/diamond_3914401.svg',
            trophyIcon: '../src/assets/icons/trophy_3914267.svg'
        }
    },
}
</script>

<style></style>