<template>
    <form @submit.prevent="signUp">
        <h1 class=" text-center text-3xl font-bold mb-5">Sign Up</h1>
        <div class=" px-5">
            <input class="form-control w-full mt-3 p-3 rounded-full outline-none"
                style="box-shadow: inset 0px 10px 15px -3px rgba(0,0,0,0.1);" type="text" placeholder="Username"
                v-model="signUp.username" />
            <input class="form-control w-full mt-3 p-3 rounded-full outline-none"
                style="box-shadow: inset 0px 10px 15px -3px rgba(0,0,0,0.1);" type="text" placeholder="Email"
                v-model="signUp.email" />
            <input class="form-control w-full mt-3 p-3 rounded-full outline-none"
                style="box-shadow: inset 0px 10px 15px -3px rgba(0,0,0,0.1);" type="password" placeholder="Password"
                v-model="signUp.password" />
        </div>
        <div class=" px-3 mt-5">
            <button type="submit" class=" w-full p-2 bg-[#4CCD99] rounded-full text-white font-medium">Sign
                up</button>
        </div>
    </form>
</template>

<script>
export default {
    name: 'signUp',
    data() {
        return {
            signUp: {
                username: '',
                email: '',
                password: ''
            }
        }
    },
}
</script>

<style></style>