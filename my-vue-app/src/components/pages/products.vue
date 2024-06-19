<template>
    <div>
        <div class="load">
            <section class="w-fit mx-auto grid grid-cols-1 lg:grid-cols-3 md:grid-cols-2 justify-items-center justify-center gap-y-20 gap-x-14 mt-10 mb-5 pb-5">
                <ItemProduct v-for="item in allProduct" :key="item.id" :product="item" />
            </section>
        </div>
    </div>
</template>
<script>
import { mapGetters, mapActions } from 'vuex'
import ItemProduct from '../Product/itemProduct.vue'

export default {
    name: 'products',
    components: {
        ItemProduct,
    },
    computed: {
        ...mapGetters(['allProduct']),
    },
    mounted() {
        window.scrollTo(0, 0);
    },
    created() {
        this.fetchProducts();
    },
    methods: {
        ...mapActions(['fetchProducts']),
    },
}
</script>

<style></style>
