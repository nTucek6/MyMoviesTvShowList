<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useAuthentication } from '@/stores/admin/authentication'
import AdminLoginDTO from '@/app/shared/models/admin-login.model'
import ErrorComponent from '@/app/shared/components/ErrorComponent.vue'

const auth = useAuthentication()
const User = ref<AdminLoginDTO>(new AdminLoginDTO())

const errorData = computed(() => auth.errorData)
const hasError = ref<boolean>(false)

const formSubmit = async () => {
  await auth.AdminLogin(User.value)
}

watch(errorData, () => {
  hasError.value = errorData.value.StatusCode > 0
})
</script>

<template>
  <ErrorComponent v-if="hasError" :message="errorData.Message" />
  <form @submit.prevent="formSubmit">
    <div class="form-group">
      <label for="username">Username</label>
      <input type="text" name="username" v-model="User.Username" />
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="password" name="password" v-model="User.Password" />
    </div>
    <button type="submit" class="btn">Login</button>
  </form>
</template>

<style scoped lang="scss">
form {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-direction: column;
  height: 100svh;
}

.form-group {
  padding: 10px 0;
}

.form-group:first-of-type {
  padding-top: 0;
}
</style>
