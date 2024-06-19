<template>
  <div>
    <h1 class="text-5xl">Dịch vụ</h1>
    <div class=" flex">
      <div class="mt-5 w-[60%]">
        <ul class="text-xl leading-loose">
          <li class="flex items-center gap-2">
            <svg class="flex-shrink-0 w-3.5 h-3.5 text-green-500 dark:text-green-400" aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 16 12" width="3rem" height="3rem">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M1 5.917 5.724 10.5 15 1.5" />
            </svg>
            <span>Chất lượng sản phẩm: <span>Sức khỏe bắt đầu từ chất lượng.</span></span>
          </li>
          <li class="flex items-center gap-2">
            <svg class="flex-shrink-0 w-3.5 h-3.5 text-green-500 dark:text-green-400" aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 16 12" width="3rem" height="3rem">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M1 5.917 5.724 10.5 15 1.5" />
            </svg>
            <span>Đóng gói: <span>Tính cẩn chăm sóc từ vườn đến bàn.</span></span>
          </li>
          <li class="flex items-center gap-2">
            <svg class="flex-shrink-0 w-3.5 h-3.5 text-green-500 dark:text-green-400" aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 16 12" width="3rem" height="3rem">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M1 5.917 5.724 10.5 15 1.5" />
            </svg>
            <span>Giao hàng: <span>Nhanh chóng và tin cậy - tận hưởng hương vị mỗi ngày.</span></span>
          </li>
          <li class="flex items-center gap-2">
            <svg class="flex-shrink-0 w-3.5 h-3.5 text-green-500 dark:text-green-400" aria-hidden="true"
              xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 16 12" width="3rem" height="3rem">
              <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M1 5.917 5.724 10.5 15 1.5" />
            </svg>
            <span>Thái độ nhân viên: <span>Tư vấn nhiệt tình, hạnh phúc mua sắm hơn.</span></span>
          </li>
        </ul>
      </div>
      <img :src="imgUrl" alt="" class=" w-[40%]">
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      imgUrl: '../src/assets/grocery-1232944_1280.jpg'
    }
  },
}
</script>

<style></style>