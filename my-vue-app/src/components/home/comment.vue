<template>
    <div id="default-carousel" class="relative w-full" data-carousel="slide">
        <div class=" px-5">
            <div class=" flex items-center justify-between px-5 pb-2 border-b-2 mb-5">
                <h2 class=" text-4xl font-semibold">Những gì mọi người nói</h2>
                <div class="flex gap-2">
                    <!-- Slider controls -->
                    <button type="button" class=" p-2 bg-yellow-400 rounded-full border border-black"
                        data-carousel-prev>
                        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 15 15">
                            <path fill="currentColor" fill-rule="evenodd"
                                d="M8.842 3.135a.5.5 0 0 1 .023.707L5.435 7.5l3.43 3.658a.5.5 0 0 1-.73.684l-3.75-4a.5.5 0 0 1 0-.684l3.75-4a.5.5 0 0 1 .707-.023"
                                clip-rule="evenodd" />
                        </svg>
                    </button>
                    <button type="button" class=" p-2 bg-yellow-400 rounded-full border border-black"
                        data-carousel-next>
                        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 15 15">
                            <path fill="currentColor" fill-rule="evenodd"
                                d="M6.158 3.135a.5.5 0 0 1 .707.023l3.75 4a.5.5 0 0 1 0 .684l-3.75 4a.5.5 0 1 1-.73-.684L9.566 7.5l-3.43-3.658a.5.5 0 0 1 .023-.707"
                                clip-rule="evenodd" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
        <!-- Carousel wrapper -->
        <div class="relative h-56 overflow-hidden rounded-lg md:h-96">
            <!-- Item -->
            <div class="hidden duration-700 ease-in-out flex justify-center" data-carousel-item
                v-for=" comment in comments" :key="comment.id">
                <div class=" w-[80%] h-fit rounded-lg p-5">
                    <h1 class=" text-3xl indent-3 leading-loose h-60">" {{ comment.text }} "</h1>
                    <div class=" bottom-0 flex gap-2 border-t-2 pt-2">
                        <img class=" rounded-full" :src="comment.imgUser" alt="" width="50px" height="50px">
                        <div>
                            <p>{{ comment.name }}</p>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>

</template>

<script>
import { onMounted } from 'vue'
import { initFlowbite } from 'flowbite'

// initialize components based on data attribute selectors
onMounted(() => {
    initFlowbite();
})
export default {
    data() {
        return {
            comments: [
                {
                    id: 1,
                    text: 'Đơn giản là văn bản giả của ngành in ấn và sắp chữ. Lorem đã không còn là văn bản giả tiêu chuẩn của ngành kể từ những năm 1500, khi một nhà in vô danh sử dụng một chiếc bếp.',
                    imgUser: 'https://www.lrg.co.uk/wp-content/uploads/2017/12/450x450.png',
                    name: 'user 1',
                },
                {
                    id: 2,
                    text: 'Đơn giản là văn bản giả của ngành in ấn và sắp chữ. Lorem đã không còn là văn bản giả tiêu chuẩn của ngành kể từ những năm 1500, khi một nhà in vô danh sử dụng một chiếc bếp.',
                    imgUser: 'https://www.lrg.co.uk/wp-content/uploads/2017/12/450x450.png',
                    name: 'user 1',
                },
                {
                    id: 3,
                    text: 'Đơn giản là văn bản giả của ngành in ấn và sắp chữ. Lorem đã không còn là văn bản giả tiêu chuẩn của ngành kể từ những năm 1500, khi một nhà in vô danh sử dụng một chiếc bếp.',
                    imgUser: 'https://www.lrg.co.uk/wp-content/uploads/2017/12/450x450.png',
                    name: 'user 1',
                },
                {
                    id: 4,
                    text: 'Đơn giản là văn bản giả của ngành in ấn và sắp chữ. Lorem đã không còn là văn bản giả tiêu chuẩn của ngành kể từ những năm 1500, khi một nhà in vô danh sử dụng một chiếc bếp.',
                    imgUser: 'https://www.lrg.co.uk/wp-content/uploads/2017/12/450x450.png',
                    name: 'user 1',
                },
                {
                    id: 5,
                    text: '',
                    imgUser: 'https://www.lrg.co.uk/wp-content/uploads/2017/12/450x450.png',
                    name: 'user 1',
                },
            ]
        }
    },
}
</script>

<style></style>