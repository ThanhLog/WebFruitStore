<template>
    <div class="itemProduct bg-white w-fit h-fit rounded-b-lg hover:scale-105 ease-out duration-700">
        <router-link :to="'/Products/' + product.ProductName" class=" relative">
            <img :src="product.Image" alt="" width="250px" height="250px" class=" rounded-t-xl" />
            <p class=" absolute top-2 left-2 bg-slate-900 px-5 py-2 rounded-full text-white font-medium">{{ product.type
                }}</p>
        </router-link>
        <div class=" mt-4 px-2 w-full">
            <router-link :to="'/Products/' + product.ProductName" class=" relative">
                <h3 class=" text-2xl">{{ product.ProductName }}</h3>
            </router-link>
            <div class=" p-2 flex w-full justify-between">
                <div>
                    <p>
                        <span v-if="product.promotion" class="text-xs line-through text-slate-400">${{ product.Price
                            }}</span>
                        <span v-if="product.promotion" class="pl-2 text-lg font-medium no-underline text-black">${{
                            product.Promotion }}</span>
                        <span v-else class="text-lg font-medium no-underline text-black">${{ product.Price }}</span>
                    </p>

                    <div class="starRating flex">
                        <img :src="starIcon" alt="">
                        <img :src="starIcon" alt="">
                        <img :src="starIcon" alt="">
                        <img :src="starHaftIcon" alt="">
                        <img :src="starEmpty" alt="">
                    </div>
                </div>
                <button @click="addToCart()" class="btn-cart p-2 w-fit h-fit rounded-full hover:bg-slate-500">
                    <svg xmlns="http://www.w3.org/2000/svg" id="Layer_1" data-name="Layer 1" viewBox="0 0 24 24"
                        width="1.3rem" height="1.3rem">
                        <path
                            d="M23.201,9.181c-.549-.636-1.313-1.042-2.142-1.151C20.511,3.512,16.658,0,12,0S3.486,3.514,2.941,8.034c-.818,.115-1.569,.519-2.113,1.147C.193,9.915-.09,10.883,.05,11.838l1.011,6.894c.441,3.004,3.108,5.269,6.203,5.269h9.502c3.095,0,5.762-2.265,6.203-5.269l1.011-6.894c.14-.955-.144-1.924-.778-2.657ZM12,3c2.99,0,5.487,2.157,6.02,5H5.98c.534-2.843,3.03-5,6.02-5Zm9.011,8.402l-1.011,6.894c-.227,1.541-1.617,2.704-3.234,2.704H7.264c-1.617,0-3.008-1.163-3.234-2.704l-1.011-6.894c-.015-.096,.013-.183,.079-.26,.046-.054,.149-.143,.324-.143H20.608c.175,0,.277,.089,.323,.142,.046,.054,.097,.142,.079,.261Zm-5.011,3.098v3c0,.828-.672,1.5-1.5,1.5s-1.5-.672-1.5-1.5v-3c0-.828,.672-1.5,1.5-1.5s1.5,.672,1.5,1.5Zm-5,0v3c0,.828-.672,1.5-1.5,1.5s-1.5-.672-1.5-1.5v-3c0-.828,.672-1.5,1.5-1.5s1.5,.672,1.5,1.5Z" />
                    </svg>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios'
import * as signalR from '@microsoft/signalr';
import { mapGetters } from 'vuex';
export default {
    name: 'itemProduct',
    props: {
        product: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            starEmpty: '../src/assets/icons/starEmpty.svg',
            starIcon: '../src/assets/icons/star.svg',
            starHaftIcon: '../src/assets/icons/starHaft.svg',
            connection: null,
        }
    },
    computed: {
        ...mapGetters(['isLogIn', 'userName']),
    },
    methods: {
        async addToCart() {
            if (!this.isLogIn) {
                this.$router.push({ path: '/SignIn' });
                return;
            }

            try {
                const payload = {
                    UserName: this.userName,
                    ProductID: this.product.ProductID,
                    ProductName: this.product.ProductName,
                    Quantity: 1,
                    Image: this.product.Image,
                    Price: this.product.Price,
                    Origin: this.product.Origin || 'VIỆT NAM'
                };

                console.log(payload);

                const response = await axios.post('http://localhost:5022/api/Carts/AddProductToCart', payload);

                if (response.status === 200) {
                    console.log('Sản phẩm đã được thêm vào giỏ hàng.');
                    // Gọi hàm cập nhật UI giỏ hàng
                    this.handleCartUpdated();
                } else {
                    console.error('Có lỗi xảy ra:', response.data);
                }
            } catch (error) {
                console.error('Lỗi khi thêm sản phẩm vào giỏ hàng:', error);
            }
        },
        handleCartUpdated() {
            // Cập nhật UI giỏ hàng tại đây
            // Ví dụ: gửi sự kiện 'CartUpdated' để thông báo cho Cart.vue
            this.$emit('cart-updated');
        }
    }
}
</script>

<style></style>