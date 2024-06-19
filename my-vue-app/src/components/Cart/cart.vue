<template>
    <div>
        <div class="mt-5 relative w-full">
            <button @click="goBack"
                class="bg-black text-white rounded-full font-medium flex items-center ml-5 px-3 py-2 z-50">
                <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 15 15">
                    <path fill="currentColor" fill-rule="evenodd"
                        d="M6.854 3.146a.5.5 0 0 1 0 .708L3.707 7H12.5a.5.5 0 0 1 0 1H3.707l3.147 3.146a.5.5 0 0 1-.708.708l-4-4a.5.5 0 0 1 0-.708l4-4a.5.5 0 0 1 .708 0"
                        clip-rule="evenodd" />
                </svg>Quay lại
            </button>
            <h1 class=" absolute top-0 left-0 w-full text-center text-3xl -z-50">Giỏ hàng</h1>
        </div>
        <div>
            <div v-if="dataCartUser && dataCartUser.length === 0"
                class=" -mt-5 text-center w-full flex flex-col items-center">
                <img :src="cartEmpty" alt="Image Cart Empty" height="90%" />
                <h1 class=" text-3xl font-semibold">Chưa có sản phẩm trong giỏ hàng</h1>
            </div>
            <div v-else class="p-5 grid grid-cols-12 gap-3 w-full">
                <!-- List Cart Item -->
                <div class="mainCart overflow-y-auto col-span-9 h-[70vh]">
                    <!-- Item Cart -->
                    <itemCart v-for="(item, index) in cartItems" :key="index" :product="item" />
                </div>
                <div class="Provisional right-0 col-span-3 border-2 w-full h-fit pt-5 px-3 pb-2">
                    <h3 class="text-3xl font-semibold">ORDER SUMMARY</h3>
                    <form @submit.prevent="checkOut" class="mt-5 font-medium">
                        <label class="w-full flex justify-between items-center mt-2">Tính tổng <span>{{
                            formatCurrency(subtotal) }}</span></label>
                        <label class="w-full flex justify-between items-center mt-2">VAT (10%) <span>{{
                            formatCurrency(vat) }}</span></label>
                        <label class="w-full text-xl flex justify-between items-center mt-4">Tổng <span>{{
                            formatCurrency(total) }}</span></label>
                        <button class="mt-5 bg-slate-800 text-white w-full p-3 rounded-xl">
                            <router-link to="/Order" class=" w-full h-full">Check
                                Out</router-link>
                        </button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import itemCart from '../Cart/itemCart.vue';
import { mapGetters, mapActions } from 'vuex';
import * as signalR from '@microsoft/signalr';

export default {
    components: { itemCart },
    data() {
        return {
            cartEmpty: '../src/assets/cartEmpty.png',
        };
    },
    computed: {
        ...mapGetters(['dataCartUser', 'isLogIn', 'userInfo', 'userName']),
        cartItems() {
            return this.dataCartUser;
        },
        subtotal() {
            if (this.cartItems.length === 0) {
                return 0; // Trả về 0 nếu giỏ hàng trống
            }
            return this.cartItems.reduce((sum, item) => sum + item.Price * item.Quantity, 0);
        },
        vat() {
            return this.subtotal * 0.1; // Tính VAT
        },
        total() {
            return this.subtotal + this.vat; // Tính tổng
        }
    },
    async created() {
        if (this.isLogIn) {
            await this.fetchUserInfoAndCart();
        }
        this.initSignalRConnection();
    },
    methods: {
        ...mapActions(['fetchDataCartUser', 'fetchUserInfo']),
        goBack() {
            this.$router.go(-1);
        },

        async fetchUserInfoAndCart() {
            try {
                if (!this.userInfo) {
                    await this.fetchUserInfo(this.userName);
                }
                if (this.userName) {
                    await this.fetchDataCartUser(this.userName);
                } else {
                    throw new Error('Missing UserName');
                }
            } catch (error) {
                console.error('Lỗi khi lấy thông tin người dùng hoặc dữ liệu giỏ hàng:', error);
            }
        },
        initSignalRConnection() {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl('http://localhost:5022/cartHub', { withCredentials: true })
                .build();

            this.connection.on('CartUpdated', () => {
                this.updateCart();
            });

            this.connection.start()
                .then(() => {
                    console.log('SignalR connected.');
                })
                .catch(err => {
                    console.error('Error while starting SignalR connection:', err);
                });

            this.connection.on('UserInfoReceived', (userInfo) => {
                console.log('Received userInfo:', userInfo);
                this.$store.commit('SET_USER_INFO', userInfo);
            });
        },
        async updateCart() {
            try {
                if (this.isLogIn) {
                    await this.$store.dispatch('fetchDataCartUser', this.userName);
                }
            } catch (error) {
                console.error('Lỗi khi cập nhật giỏ hàng:', error);
            }
        },
        async removeItemFromCart(productId) {
            try {
                if (!this.userName) throw new Error('Missing UserName');

                const response = await axios.delete(`http://localhost:5022/api/Carts/RemoveCart`, {
                    params: {
                        UserName: this.userName,
                        ProductId: productId,
                    }
                });

                console.log('Item removed from cart:', response.data);
                await this.updateCart();
            } catch (error) {
                console.error('Error removing item from cart:', error);
            }
        },
        formatCurrency(value) {
            return new Intl.NumberFormat('vi-VN', { style: 'currency', currency: 'VND' }).format(value);
        }
    }
};
</script>




<style>
input {
    outline: none;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
    -webkit-appearance: none;
}
</style>
