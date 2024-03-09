<script setup lang="ts">
import { ref } from 'vue'
import { useAuthentication } from '@/stores/Authentication/authentication'
import { UserLoginDTO } from '@/app/shared/models/user-login.model';

const User = ref<UserLoginDTO>(new UserLoginDTO)

const signInButtonPressed = async () => {
  await useAuthentication().Login(User.value)
}
</script>

<template>
  <section id="login-form">
    <div id="title">
      <h1>Login</h1>
    </div>

    <form @submit.prevent="signInButtonPressed">
      <div class="form-group mb-3">
        <input type="email" v-model="User.Email" class="w-50" id="email" placeholder="Email" />
      </div>
      <div class="form-group mb-3">
        <input
          type="password"
          v-model="User.Password"
          class="w-50"
          id="password"
          placeholder="Password"
        />
      </div>

      <div class="mb-5">
        <RouterLink class="forgotPassword" to=""><small>Forgot password?</small></RouterLink>
      </div>

      <button type="submit" class="btn w-25">Login</button>
    </form>
  </section>
</template>

<style scoped>
@import url('./login.scss');
</style>
