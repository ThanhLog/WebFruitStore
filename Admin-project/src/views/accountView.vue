<template>
    <div>
        <itemAccountVue :accounts="accounts" />
    </div>
</template>

<script>
import axios from 'axios';
import itemAccountVue from '../components/Account/itemAccount.vue';

export default {
    name: 'accountView',
    components: { itemAccountVue },
    data() {
        return {
            accounts: []
        };
    },
    mounted() {
        // Gửi yêu cầu GET đến API để lấy danh sách tài khoản
        axios.get('http://localhost:5022/api/Accounts')
            .then(response => {
                // Lưu trữ danh sách tài khoản nhận được từ API vào biến accounts
                this.accounts = response.data;
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    }
}
</script>

<style></style>
