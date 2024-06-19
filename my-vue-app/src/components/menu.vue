<template>
    <div class=" sticky top-0 left-0 bg-white z-50">
        <div class="Menu relative flex justify-evenly items-center text-xl pt-2 pb-2"
            style="box-shadow: -4px 14px 9px -8px rgba(0,0,0,0.1);">
            <routerLink to="/" class=" text-3xl font-semibold">Localhost</routerLink>
            <ul class="itemMenu flex gap-[3em]" :class="{ 'active': isMenuOpen }">
                <li>
                    <routerLink @click="closeMenuIfMobile" to="/">Home</routerLink>
                </li>
                <li>
                    <routerLink @click="closeMenuIfMobile" to="/Products">Products</routerLink>
                </li>
                <li>
                    <routerLink @click="closeMenuIfMobile" to="/Blog">Blog</routerLink>
                </li>
                <li>
                    <routerLink @click="closeMenuIfMobile" to="/About">About</routerLink>
                </li>
                <li>
                    <routerLink @click="closeMenuIfMobile" to="/Contact">Contact</routerLink>
                </li>
            </ul>
            <div class=" flex items-center gap-[10px]">
                <button @click="OpenCart" class="Cart relative">
                    <img :src="cartIcon" alt="Icon Cart" />
                </button>
                <div>
                    <router-link v-if="!isLogIn" to="/SignIn">
                        <span class="login text-white px-4 py-2 rounded-full bg-[#211951]">Sign in/ Sign up</span>
                    </router-link>
                    <div v-else>
                        <div class="UserBtn" @mouseover="showTab = true" @mouseleave="showTab = false">
                            <button class=" bg-slate-200 px-2 py-1 rounded-full ml-5 shadow-2xl">
                                <router-link to="/userDetail" class=" flex items-center gap-2">
                                    <!-- Hiển thị Image của người dùng -->
                                    <img :src="userInfo && userInfo.length > 0 ? userInfo[0].Image : ''"
                                        alt="User Image" class="bg-black rounded-full object-cover w-[35px] h-[35px]" />
                                    <!-- Hiển thị UserName của người dùng -->
                                    <p v-if="userInfo && userInfo.length > 0">{{ userInfo[0].UserName }}</p>
                                </router-link>
                            </button>
                            <div v-if="showTab" class=" TabUser absolute z-10">
                                <tabUser />
                            </div>
                        </div>
                    </div>
                </div>
                <button class="menuBar" @click="toggleMenu">
                    <img :src="barIcon" alt="Icon MenuBar" />
                </button>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions, mapGetters } from "vuex";
import tabUser from "./user/tabUser.vue";

export default {
    name: 'menu',
    components: { tabUser },
    data() {
        return {
            showAuth: null,
            barIcon: '../src/assets/icons/menuBar.svg',
            cartIcon: '../src/assets/icons/cart.svg',
            isMenuOpen: false,
            showTab: false,
        }
    },
    computed: {
        ...mapGetters(["userInfo", "isLogIn", "userName", "dataCartUser"]),
        cartLength() {
            return this.dataCartUser.length;
        },
    },
    mounted() {
        this.callMenu();
        window.addEventListener('resize', this.callMenu);
    },
    methods: {
        ...mapActions(["fetchUserInfo"]),
        callMenu() {
            if (window.innerWidth > 481) {
                this.isMenuOpen = true;
            } else {
                this.isMenuOpen = false;
            }
        },
        toggleMenu() {
            this.isMenuOpen = !this.isMenuOpen;
        },
        closeMenuIfMobile() {
            if (window.innerWidth <= 481) {
                this.isMenuOpen = false;
                window.scrollTo({
                    top: 0,
                    behavior: 'smooth'
                });
            }
        },
        toggleAuth() {
            this.showAuth = this.showAuth === 'dashboard' ? null : 'dashboard';
        },

        OpenCart() {
            if (!this.isLogIn) {
                this.$router.push({ path: '/SignIn' });
                return;
            } else {
                this.$router.push({ path: '/Cart' });
            }
        }
    },
    watch: {
        isLogIn: {
            immediate: true,
            handler(newValue) {
                if (newValue) {
                    this.fetchUserInfo(this.userName)
                        .then(() => {
                            const user = this.userInfo[0];
                            if (user) {
                                const { UserName, Image } = user;
                                console.log("UserName:", UserName);
                                console.log("Image:", Image);
                            }
                        });
                    console.log("Người dùng đăng nhập");
                } else {
                    console.log("Người dùng chưa đăng nhập");
                }
            }
        }
    },
    beforeDestroy() {
        window.removeEventListener('resize', this.callMenu);
    }
}
</script>


<style>
.menuBar {
    display: none;
}

.itemMenu {
    display: none;
}

.itemMenu.active {
    display: flex;
}

.UserBtn:hover .TabUser {
    display: block;
}

@media only screen and (max-device-width: 481px) {

    /* styles for mobile browsers smaller than 480px; (iPhone) */
    .menuBar {
        display: block;
    }

    .itemMenu::before {
        content: '';
        height: 10px;
        width: 100%;
    }

    .itemMenu {
        width: 100%;
        height: 100%;
        background: white;
        position: absolute;
        top: 100%;
        flex-direction: column;
        align-items: center;
    }
}

/* different techniques for iPad screening */
@media only screen and (min-device-width: 481px) and (max-device-width: 1024px) and (orientation:portrait) {

    /* For portrait layouts only */
    .menuBar {
        display: block;
    }

    .itemMenu {
        padding: 10px 10px;
        width: 100%;
        background: white;
        position: absolute;

        top: 100%;
        flex-direction: column;
        align-items: center;
    }
}
</style>