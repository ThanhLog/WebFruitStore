<template>
    <div class=" w-full">
        <div class=" overflow-x-auto h-72 w-full">
            <table class="table-fixed w-full">
                <thead class="bg-black text-white sticky top-0">
                    <tr class=" text-white text-[13px]">
                        <th class="text-center px-8 py-2">Image</th>
                        <th class="text-center px-8 py-2">Product Name</th>
                        <th class="text-center px-8 py-2">Price</th>
                        <th class="text-center px-8 py-2">Quantity</th>
                        <th class="text-center px-8 py-2">Total</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="text-center border-t-[2px]" v-for="(item, index) in cartItems" :key="index">
                        <td class=" py-2"><img :src="item.Image" alt="Product Image" width="100"></td>
                        <td class=" py-2 ">{{ item.ProductName }}</td>
                        <td class=" py-2 ">{{ item.Price }}</td>
                        <td class="  py-2">{{ item.Quantity }}</td>
                        <td class="  py-2">{{ item.Price * item.Quantity }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>

<script>
export default {
    name: 'CartTable',
    props: {
        cartItems: {
            type: Array,
            required: true
        }
    }
};
</script>
