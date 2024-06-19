<template>
    <div>
        <div>
            <button @click="goBack"
                class=" mt-3 ml-3 bg-black text-white rounded-full font-medium flex items-center px-3 py-2">
                <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 15 15">
                    <path fill="currentColor" fill-rule="evenodd"
                        d="M6.854 3.146a.5.5 0 0 1 0 .708L3.707 7H12.5a.5.5 0 0 1 0 1H3.707l3.147 3.146a.5.5 0 0 1-.708.708l-4-4a.5.5 0 0 1 0-.708l4-4a.5.5 0 0 1 .708 0"
                        clip-rule="evenodd" />
                </svg>Quay lại
            </button>
            <!-- Item Product Detail -->
            <div class=" p-5 flex gap-5" v-for="item in productDetail" :key="item.productId">
                <img :src="item.Image" alt="" width="350px" height="350px">
                <div>
                    <h1 class=" text-5xl">{{ item.ProductName }}</h1>
                    <div class=" mt-5 text-xl">
                        <p>Xuất xứ: {{ item.Origin }}</p>
                        <div class=" mt-3">
                            <div class=" flex gap-3">
                                Đánh giá:
                                <div class="starRating flex">
                                    <img :src="starIcon" alt="">
                                    <img :src="starIcon" alt="">
                                    <img :src="starIcon" alt="">
                                    <img :src="starHaftIcon" alt="">
                                    <img :src="starEmpty" alt="">
                                </div>
                            </div>
                            <p class=" mt-5"> Giá:
                                <span v-if="item.Promotion" class="text-lg line-through text-slate-400">${{
                                    item.Price
                                    }}</span>
                                <span v-if="item.Promotion" class="pl-2 text-xl font-medium no-underline text-black">${{
                                    item.Promotion }}</span>
                                <span v-else class="text-xl font-medium no-underline text-black">${{ item.Price
                                    }}</span>
                            </p>
                        </div>
                        <div class="p-0 h-fit w-fit flex items-center mt-5 border-2 bg-black border-black rounded-lg">
                            <button @click="minus" class=" text-xl font-bold text-white p-2 ">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24">
                                    <path fill="currentColor" d="M19 12.998H5v-2h14z" />
                                </svg>
                            </button>

                            <input type="number" v-model="quantity"
                                class=" text-end w-[60px] outline-none border-none" />

                            <button @click="add" class=" text-xl font-bold text-white p-2 ">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24">
                                    <path fill="currentColor" d="M19 12.998h-6v6h-2v-6H5v-2h6v-6h2v6h6z" />
                                </svg>
                            </button>
                        </div>
                        <div class=" mt-10">
                            <button class=" bg-black text-white px-5 py-2 rounded-full flex items-center gap-5">
                                <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em"
                                    viewBox="0 0 24 24">
                                    <path fill="currentColor"
                                        d="M17 18c-1.11 0-2 .89-2 2a2 2 0 0 0 2 2a2 2 0 0 0 2-2a2 2 0 0 0-2-2M1 2v2h2l3.6 7.59l-1.36 2.45c-.15.28-.24.61-.24.96a2 2 0 0 0 2 2h12v-2H7.42a.25.25 0 0 1-.25-.25q0-.075.03-.12L8.1 13h7.45c.75 0 1.41-.42 1.75-1.03l3.58-6.47c.07-.16.12-.33.12-.5a1 1 0 0 0-1-1H5.21l-.94-2M7 18c-1.11 0-2 .89-2 2a2 2 0 0 0 2 2a2 2 0 0 0 2-2a2 2 0 0 0-2-2" />
                                </svg>
                                ADD TO CART
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Review Product -->
            <div class="w-full">
                <ul class="flex justify-center gap-5 text-2xl mt-5 font-semibold">
                    <li><button @click="activeTab = 'Describe'" :class="{ 'active': activeTab === 'Describe' }">Mô
                            tả</button></li>
                    <li><button @click="activeTab = 'Evaluate'" :class="{ 'active': activeTab === 'Evaluate' }">Đánh
                            giá</button></li>
                    <li><button @click="activeTab = 'Question'" :class="{ 'active': activeTab === 'Question' }">Câu hỏi
                            thường gặp</button></li>
                </ul>
                <div class="content w-full flex justify-center mt-5">
                    <div class="w-[90%] border border-[#FFC100] p-2 min-h-[150px]">
                        <div v-if="activeTab === 'Describe'" class="tab">Mô tả</div>
                        <div v-else-if="activeTab === 'Evaluate'" class="tab">Đánh giá</div>
                        <div v-else class="tab">Câu hỏi thường gặp</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
export default {
    name: 'productDetail',
    data() {
        return {
            starEmpty: '../src/assets/icons/starEmpty.svg',
            starIcon: '../src/assets/icons/star.svg',
            starHaftIcon: '../src/assets/icons/starHaft.svg',
            quantity: 1,
            activeTab: 'Describe',
        }
    },
    computed: {
        ...mapGetters(['productDetail']),
    },
    methods: {
        ...mapActions(['fetchProductDetail']),
        add: function () {
            this.quantity++
        },
        minus: function () {
            if (this.quantity != 1) {
                this.quantity--;
            }
        },
        goBack() {
            this.$router.go(-1);
        },
    },
    mounted() {
        this.fetchProductDetail(this.$route.params.ProductName);
        console.log(this.$route.params.ProductName);
    },
}
</script>

<style scoped>
input {
    outline: none;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none;
}

.active {
    color: #FFC100;
    border-bottom: 2px solid #FFC100;
    padding-bottom: 10px;
}
</style>