<template>
    <div>
        <div class=" flex items-center p-3">
            <button @click="goBack" class="">Quay lại</button>
            <h1 class=" text-3xl p-5">Create New Product</h1>
        </div>
        <form @submit.prevent="createProduct">
            <div>
                <label for="image">Image URL:</label>
                <input type="text" v-model="product.image" id="image" />
            </div>
            <div>
                <label for="productName">Product Name:</label>
                <input type="text" v-model="product.productName" id="productName" />
            </div>
            <div>
                <label for="origin">Origin:</label>
                <input type="text" v-model="product.origin" id="origin" />
            </div>
            <div>
                <label for="price">Price:</label>
                <input type="number" v-model="product.price" id="price" />
            </div>
            <div>
                <label for="sold">Sold:</label>
                <input type="number" v-model="product.sold" id="sold" />
            </div>
            <div>
                <label for="stock">Stock:</label>
                <input type="number" v-model="product.stock" id="stock" />
            </div>
            <div>
                <label for="imported">Imported:</label>
                <input type="number" v-model="product.imported" id="imported" />
            </div>
            <button type="submit">Create Product</button>
        </form>
    </div>
</template>

<script>
import axios from 'axios';

export default {
    data() {
        return {
            product: {
                productID: 0,
                image: '',
                productName: '',
                origin: '',
                price: 0,
                sold: 0,
                stock: 0,
                imported: 0
            }
        };
    },
    methods: {
        async createProduct() {
            try {
                const response = await axios.post('http://localhost:5022/api/Products', this.product, {
                    headers: {
                        'Content-Type': 'application/json-patch+json',
                        'accept': '*/*'
                    }
                });
                console.log(response.data);
                alert('Product created successfully');
            } catch (error) {
                console.error(error);
                alert('Failed to create product');
            }
        },
        goBack() {
            this.$router.go(-1)
        }
    }
};
</script>

<style scoped>
form {
    max-width: 600px;
    margin: auto;
    display: flex;
    flex-direction: column;
}

div {
    margin-bottom: 10px;
}

label {
    margin-bottom: 5px;
}

input {
    padding: 8px;
    font-size: 16px;
}

button {
    padding: 10px;
    font-size: 16px;
    background-color: #42b983;
    color: white;
    border: none;
    cursor: pointer;
}

button:hover {
    background-color: #359a6c;
}
</style>
