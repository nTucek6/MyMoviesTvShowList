<script setup lang="ts">
import { ref } from 'vue'
import { useAuthentication } from '@/stores/Authentication/authentication'
import { UserRegisterDTO } from '@/app/shared/models/user-register.model'

const PasswordConfirm = ref('')

const User = ref<UserRegisterDTO>(new UserRegisterDTO())

const signInButtonPressed = async () => {
  await useAuthentication().Register(User.value)
}
</script>

<template>
  <section>
    <h1>Register</h1>
    <hr class="mt-5 mb-5" />
    <form @submit.prevent="signInButtonPressed">
      <div class="form-group mb-3">
        <input
          type="email"
          v-model="User.Email"
          class="w-50"
          id="email"
          placeholder="Email"
          required
        />
      </div>
      <div class="form-group mb-3">
        <input
          type="text"
          v-model="User.Username"
          class="w-50"
          id="username"
          placeholder="Username"
          required
        />
      </div>
      <div class="form-group mb-3">
        <input
          type="password"
          v-model="User.Password"
          class="w-50"
          id="password"
          placeholder="Password"
          required
        />
      </div>
      <div class="form-group mb-3">
        <input
          type="password"
          v-model="PasswordConfirm"
          class="w-50"
          id="passwordRepeat"
          placeholder="Confirm Password"
          required
        />
      </div>

      <button type="submit" class="btn w-25">Register</button>
    </form>
  </section>
</template>

<style scoped>
@import url('./register.scss');
</style>
