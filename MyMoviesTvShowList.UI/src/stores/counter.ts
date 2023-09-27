import { ref, computed } from 'vue'
import { defineStore } from 'pinia'

export const useCounterStore = defineStore('counter', () => {
  const count = ref(0)
  const doubleCount = computed(() => count.value * 2)
  async function increment(): Promise<any> {
    const res = await fetch("http://localhost:8080/hello")
    return await res.json()
  }

  return { count, doubleCount, increment }
})
