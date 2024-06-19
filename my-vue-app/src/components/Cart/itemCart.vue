<!-- itemCart.vue -->
<template>
    <div class="itemCart flex gap-[20px] mt-5 pt-5 shadow-lg shadow-black-500/40">
        <img :src="product.Image" alt="" width="150px" height="150px">
        <div class="flex w-full items-start">
            <div class="titleProduct w-full">
                <h3 class="text-3xl font-medium">{{ product.ProductName }}</h3>
                <div class="mt-5">
                    <p>Giá: {{ product.Price }}</p>
                    <div class="p-0 h-fit w-fit flex items-center mt-5 border-2 bg-black border-black rounded-lg">
                        <button @click="minus" class="text-lg font-bold text-white p-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24">
                                <path fill="currentColor" d="M19 12.998H5v-2h14z" />
                            </svg>
                        </button>
                        <input type="number" v-model="inputQuantity" @input="updateQuantity"
                            class="text-end w-[60px] outline-none border-none" />
                        <button @click="add" class="text-lg font-bold text-white p-1">
                            <svg xmlns="http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 24 24">
                                <path fill="currentColor" d="M19 12.998h-6v6h-2v-6H5v-2h6v-6h2v6h6z" />
                            </svg>
                        </button>
                    </div>
                </div>
            </div>
            <div>
                <button @click="removeFromCart" class="removeCart">
                    <svg xmlns="http://www.w3.org/2000/svg" width="1.5em" height="1.5em" viewBox="0 0 16 16">
                        <path fill="currentColor" fill-rule="evenodd"
                            d="M5.75 3V1.5h4.5V3zm-1.5 0V1a1 1 0 0 1 1-1h5.5a1 1 0 0 1 1 1v2h2.5a.75.75 0 0 1 0 1.5h-.365l-.743 9.653A2 2 0 0 1 11.148 16H4.852a2 2 0 0 1-1.994-1.847L2.115 4.5H1.75a.75.75 0 0 1 0-1.5zm-.63 1.5h8.76l-.734 9.538a.5.5 0 0 1-.498.462H4.852a.5.5 0 0 1-.498-.462z"
                            clip-rule="evenodd" />
                    </svg>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';

export default {
    props: {
        product: {
            type: Object,
            required: true
        }
    },
    data() {
        return {
            inputQuantity: this.product.Quantity
        }
    },
    computed: {
        ...mapGetters(['userName'])
    },
    methods: {
        ...mapActions(['removeItemFromCart', 'updateProductQuantity']),
        async updateQuantity() {
            const quantity = parseInt(this.inputQuantity);
            const UserName = this.userName;
            const ProductName = this.product.ProductName;
            const payload = { UserName, ProductName, Quantity: quantity };
            console.log("Dispatching updateProductQuantity with:", payload);
            try {
                await this.updateProductQuantity(payload);
            } catch (error) {
                console.error("Failed to update quantity in component:", error);
                if (error.response) {
                    console.error("Error response data:", error.response.data);
                    console.error("Error response status:", error.response.status);
                    console.error("Error response headers:", error.response.headers);
                }
            }
        },
        add() {
            this.inputQuantity++;
            this.updateQuantity();
        },
        minus() {
            if (this.inputQuantity > 1) {
                this.inputQuantity--;
                this.updateQuantity();
            }
        },
        async removeFromCart() {
            const ProductName = this.product.ProductName; // Lấy ProductName từ props product
            const UserName = this.userName;
            if (confirm(`Are you sure you want to remove ${ProductName} from the cart?`)) {
                try {
                    await this.removeItemFromCart({ productName: ProductName, UserName: UserName });
                } catch (error) {
                    console.error("Error removing item from cart:", error);
                }
            }
        },
    },
    created() {
        this.$store.subscribe((mutation, state) => {
            if (mutation.type === "UPDATE_CART_ITEM_REALTIME") {
                if (mutation.payload.productName === this.product.ProductName) {
                    this.inputQuantity = mutation.payload.quantity;
                }
            }
        });
    }
}
</script>



