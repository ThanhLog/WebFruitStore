<template>
    <div class=" flex justify-center items-center gap-2 h-full w-full">
        <div class="loader"></div>
    </div>
</template>

<script>
export default {

}
</script>

<style>
/* HTML: <div class="loader"></div> */
.loader {
    width: 100px;
    height: 40px;
    --g: radial-gradient(farthest-side, #0000 calc(95% - 3px), #000000 calc(100% - 3px) 98%, #0000 101%) no-repeat;
    background: var(--g), var(--g), var(--g);
    background-size: 30px 30px;
    animation: l9 1s infinite alternate;
}

@keyframes l9 {
    0% {
        background-position: 0 50%, 50% 50%, 100% 50%;
    }

    20% {
        background-position: 0 0, 50% 50%, 100% 50%;
    }

    40% {
        background-position: 0 100%, 50% 0, 100% 50%;
    }

    60% {
        background-position: 0 50%, 50% 100%, 100% 0;
    }

    80% {
        background-position: 0 50%, 50% 50%, 100% 100%;
    }

    100% {
        background-position: 0 50%, 50% 50%, 100% 50%;
    }
}
</style>