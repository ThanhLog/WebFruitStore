<template>
    <div class=" relative">
        <div class="banner w-full flex h-[100vh] items-center">
            <div class="ContentBanner ml-4 w-[80%]">
                <div class=" text-7xl subpixel-antialiased  ">
                    <h1>Eat <span class="healthy px-5 py-1 text-white rounded-full">Healthy</span> &</h1>
                    <h1 class=" mt-3">Feel Great with</h1>
                    <h1 class=" mt-3">Our <span class=" text-yellow-400">Diet Plans</span></h1>
                </div>
                <p class=" mt-5">Lorem ipsum, dolor sit amet consectetur adipisicing elit. Ipsa, harum minima.
                    Dignissimos eum sed
                    laborum inventore facilis neque qui sunt!
                </p>
                <div class=" w-full translate-x-[10%]">
                    <router-link to="/Products"
                        class="w-fit mt-10 text-2xl bg-black text-white flex items-center h-fit p-3 rounded-full">
                        <span class="rounded-lg px-2">Oder Now</span>

                        <span><svg xmlns=" http://www.w3.org/2000/svg" width="1em" height="1em" viewBox="0 0 15 15">
                                <path fill="currentColor" fill-rule="evenodd"
                                    d="M8.146 3.146a.5.5 0 0 1 .708 0l4 4a.5.5 0 0 1 0 .708l-4 4a.5.5 0 0 1-.708-.708L11.293 8H2.5a.5.5 0 0 1 0-1h8.793L8.146 3.854a.5.5 0 0 1 0-.708"
                                    clip-rule="evenodd" />
                            </svg></span>
                    </router-link>
                </div>
            </div>
            <img class=" object-cover rotate-180 w-[40%] h-full" :src="imgBanner" alt="" />
        </div>
        <div class=" absolute w-full bottom-0 z-20 bg-slate-900 text-white px-2 py-2">
            <div class=" w-full h-fit overflow-hidden relative">
                <div class="slide flex text-4xl gap-[20px]" ref="slideshow">
                    <p v-for="(item, index) in items" :key="index">{{ item }} &#8226;</p>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Banner',
    data() {
        return {
            imgBanner: '../src/assets/BannerFruit.png',
            items: ['BANANA', 'APPLE', 'STRAWBERRIES', 'WATERLEMON', 'BANANA', 'APPLE', 'STRAWBERRIES', 'WATERLEMON', 'BANANA', 'APPLE', 'STRAWBERRIES', 'WATERLEMON'],
        }
    },
}
</script>

<style scoped>
.healthy {
    background-image: url('../src/assets/healthy.jpg');
    background-color: rgba(165, 165, 165, 0.384);
}

.slide {
    white-space: nowrap;
    animation: slide 10s linear infinite;
}

@keyframes slide {
    0% {
        transform: translateX(0%);
    }

    50% {
        transform: translateX(-50%);
    }

    100% {
        transform: translateX(-100%);
    }
}
</style>