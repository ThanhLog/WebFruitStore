<template>
  <div class="p-5">
    <h2 class="text-3xl">Danh sách tài khoản</h2>
    <ul class="mt-5 grid grid-cols-3 gap-4">
      <li v-for="(account, index) in accounts" :key="index" class="border p-2 rounded-lg shadow-2xl">
        <div class="flex gap-3 items-center">
          <img :src="account.Image ? account.Image : defaultImage" alt="User Image"
            class="w-[100px] h-[100px] object-cover rounded-full">
          <div>
            <div>UserName: {{ account.UserName }}</div>
            <div class="mt-2">Email: {{ account.Email }}</div>
            <div class="mt-2">Password: {{ account.Password }}</div>
          </div>
        </div>
        <p>Ngày tạo tài khoản: {{ formatDate(account.CreateDate) }}</p>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ItemAccount',
  props: {
    accounts: {
      type: Array,
      required: true
    }
  },
  data() {
    return {
      defaultImage: 'https://scontent.fhan2-5.fna.fbcdn.net/v/t1.30497-1/143086968_2856368904622192_1959732218791162458_n.png?_nc_cat=1&ccb=1-7&_nc_sid=5f2048&_nc_ohc=Yg86lEgr_IkQ7kNvgEMVIe4&_nc_ht=scontent.fhan2-5.fna&oh=00_AYAHjK4LKcgIAFjK9193K3qgntEe9XLcwQ8itCEs9Totjg&oe=668EA0F8'
    };
  },
  methods: {
    async fetchUserImage(userName) {
      try {
        const response = await axios.get(`http://localhost:5022/api/Accounts/${userName}`);
        const user = response.data[0];
        return user.Image || this.defaultImage;
      } catch (error) {
        console.error('Error fetching user image:', error);
        return this.defaultImage;
      }
    },
    async loadImages() {
      for (let account of this.accounts) {
        account.Image = await this.fetchUserImage(account.UserName);
      }
    },
    formatDate(dateString) {
      const options = { year: 'numeric', month: 'long', day: 'numeric' };
      const date = new Date(dateString);
      return date.toLocaleDateString('vi-VN', options);
    }
  },
  mounted() {
    this.loadImages();
  }
};
</script>
