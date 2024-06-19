<template>
  <form @submit.prevent="submitSignIn">
    <h1 class="text-center text-3xl font-bold">Sign In</h1>
    <div class="flex justify-center gap-[10px] mt-8">
      <img :src="facebook" alt="Icon Facebook" class="border-2 p-[2px] rounded-lg">
      <img :src="google" alt="Icon Google" class="border-2 p-[2px] rounded-lg">
      <img :src="apple" alt="Icon Apple" class="border-2 p-[2px] rounded-lg">
    </div>
    <div class="flex justify-center items-center gap-1 my-2">
      <div class="bar"></div>
      <p class="or text-center text-xl text-[#b8bbc3]">Or</p>
      <div class="bar"></div>
    </div>
    <div>
      <div class="mb-3 px-5">
        <input type="text" class="form-control w-full mt-3 px-5 py-4 rounded-full outline-none"
          style="box-shadow: inset 0px 10px 15px -3px rgba(0,0,0,0.1);" name="User name" id="UserName"
          placeholder="User Name" v-model="LogIn.UserName" />
        <input type="password" class="form-control w-full mt-3 px-5 py-4 rounded-full outline-none"
          style="box-shadow: inset 0px 10px 15px -3px rgba(0,0,0,0.1);" name="password" id="password"
          placeholder="Password" v-model="LogIn.password" />
      </div>
    </div>
    <div class="px-3 mt-2">
      <button type="submit" class="w-full p-2 bg-[#4CCD99] rounded-full text-white font-medium">Sign in</button>
    </div>
  </form>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  name: 'signIn',
  data() {
    return {
      facebook: '../src/assets/icons/facebookIcon.svg',
      google: '../src/assets/icons/googleIcon.svg',
      apple: '../src/assets/icons/appleIcon.svg',
      LogIn: {
        UserName: '',
        password: ''
      },
    }
  },

  mounted() {
    // Cuộn trang lên đầu khi component được mount
    window.scrollTo(0, 0);
  },

  methods: {
    ...mapActions(['signIn']),
    async submitSignIn() {
      try {
        await this.signIn({ UserName: this.LogIn.UserName, password: this.LogIn.password });
      } catch (error) {
        console.error('Login error:', error);
      }
    },
  },
}
</script>

<style>
.bar {
  content: '';
  display: block;
  width: 25%;
  height: 2px;
  background: #ECEFF7;
}
</style>
