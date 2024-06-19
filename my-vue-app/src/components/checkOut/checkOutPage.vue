<template>
    <div>
        <button @click="goBack"
            class="bg-black text-white rounded-full font-medium flex items-center px-3 py-2 z-50 mt-5 ml-3">
            <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 15 15">
                <path fill="currentColor" fill-rule="evenodd"
                    d="M6.854 3.146a.5.5 0 0 1 0 .708L3.707 7H12.5a.5.5 0 0 1 0 1H3.707l3.147 3.146a.5.5 0 0 1-.708.708l-4-4a.5.5 0 0 1 0-.708l4-4a.5.5 0 0 1 .708 0"
                    clip-rule="evenodd" />
            </svg>Quay lại
        </button>
        <div class="p-10 flex gap-[20px]">
            <div class="w-2/3">
                <CartTableVue :cartItems="cartItems" />
            </div>
            <div class="w-1/3">
                <BuyerInfo :buyerInfo="buyerInfo" :totalOrder="totalOrder" :VAT="VAT" :totalPayment="totalPayment"
                    @updateBuyerInfo="updateBuyerInfo" @submitOrder="submitOrder" />
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex';
import BuyerInfo from './BuyerInfo.vue';
import CartTableVue from './CartTable.vue';

export default {
    name: 'CheckOutPage',
    components: { CartTableVue, BuyerInfo },
    data() {
        return {
            buyerInfo: {
                name: '',
                phone: '',
                email: '',
                UserName: this.$store.getters.userName // Lấy UserName từ store
            },
            VATRate: 0.1 // Tỷ lệ VAT
        }
    },
    computed: {
        ...mapGetters(['dataCartUser']),
        ...mapState(['Cart']),
        cartItems() {
            return this.Cart;
        },
        totalOrder() {
            return this.cartItems.reduce((total, item) => total + (item.Price * item.Quantity), 0);
        },
        VAT() {
            return this.totalOrder * this.VATRate;
        },
        totalPayment() {
            return this.totalOrder + this.VAT;
        }
    },
    methods: {
        goBack() {
            this.$router.go(-1);
        },
        updateBuyerInfo(updatedInfo) {
            this.buyerInfo = updatedInfo;
        },
        async submitOrder(orderData) {
            try {
                await this.$store.dispatch('createOrder', orderData);
                this.$router.push('/success');
            } catch (error) {
                console.error("Error submitting order:", error);
            }
        }
    }
}
</script>

<style>
/* Your styles here */
</style>
