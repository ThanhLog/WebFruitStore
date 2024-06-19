<template>
    <div class=" w-full px-10">
        <div class="flex w-full justify-center items-center gap-10 border-b-2 pb-2">
            <h3 class=" text-3xl font-semibold">Sản phẩm của chúng tôi</h3>
            <p>Chúng tôi luôn cố gắng đảm bảo rằng khách hàng hài lòng với dịch vụ của chúng tôi</p>
            <router-link to="/Products"
                class=" border-2 border-black text-lg px-3 py-2 rounded-full hover:bg-[#ECEFF7]">Xem tất
                cả</router-link>
        </div>
        <div class=" mt-5 w-full h-full relative">
            <div class=" w-[100%] h-full  overflow-x-hidden">
                <div class="w-[200%] flex h-full items-center  gap-[20px] p-5"
                    :style="{ transform: 'translateX(-' + scrollPosition + 'px)' }">
                    <ItemProduct v-for="item in topsell" :key="item.id" :product="item"/>
                    <router-link to="/Products" class=" flex items-center gap-[15px] rounded-full p-2 bg-white w-[250px]"
                        style="box-shadow: 0px 10px 15px -3px rgba(0,0,0,0.1);">
                        Xem tất cả
                        <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 15 15">
                            <path fill="currentColor" fill-rule="evenodd"
                                d="M8.146 3.146a.5.5 0 0 1 .708 0l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L11.293 8H2.5a.5.5 0 0 1 0-1h8.793L8.146 3.854a.5.5 0 0 1 0-.708"
                                clip-rule="evenodd" />
                        </svg>
                    </router-link>
                </div>
            </div>
            <button
                class="btn-prev float-left absolute top-[50%] ml-2 left-0 z-30 bg-white shadow-2xl shadow-black rounded-full"
                @click="scrollLeft">
                <svg xmlns="http://www.w3.org/2000/svg" width="2em" height="2em" viewBox="0 0 15 15">
                    <path fill="currentColor" fill-rule="evenodd"
                        d="M6.854 3.146a.5.5 0 0 1 0 .708L3.707 7H12.5a.5.5 0 0 1 0 1H3.707l3.147 3.146a.5.5 0 0 1-.708.708l-4-4a.5.5 0 0 1 0-.708l4-4a.5.5 0 0 1 .708 0"
                        clip-rule="evenodd" />
                </svg>
            </button>
            <button
                class="btn-next float-right absolute top-[50%] mr-2 right-0 z-30 bg-white shadow-2xl shadow-black rounded-full"
                @click="scrollRight">
                <svg xmlns="http://www.w3.org/2000/svg" width="2em" height="2em" viewBox="0 0 15 15">
                    <path fill="currentColor" fill-rule="evenodd"
                        d="M8.146 3.146a.5.5 0 0 1 .708 0l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L11.293 8H2.5a.5.5 0 0 1 0-1h8.793L8.146 3.854a.5.5 0 0 1 0-.708"
                        clip-rule="evenodd" />
                </svg>
            </button>
        </div>
    </div>
</template>

<script>
import ItemProduct from '../Product/itemProduct.vue';
import { mapGetters, mapActions } from 'vuex';
export default {
    components: { ItemProduct },
    data() {
        return {
            scrollPosition: 0,
            itemWidth: 200,
        }
    },
    computed: {
        ...mapGetters(["topsell"])
    },
    mounted() {
        this.fetchTopSellProduct();
    },
    methods: {
        ...mapActions(["fetchTopSellProduct"]),
        scrollLeft() {
            this.scrollPosition = 0;
        },
        scrollRight() {
            const maxPosition = (this.topsell.length - 1) * this.itemWidth;
            this.scrollPosition = maxPosition;
        }

    },
}
</script>

<style></style>