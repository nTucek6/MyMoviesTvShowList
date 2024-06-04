<script setup lang="ts">
import { ref } from 'vue'
import { useAuthentication } from '@/stores/authentication'
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

<style scoped lang="scss">
section {
  //display: flex;
  border: 1px #fff;
  border-radius: 10px;
  text-align: center;
  padding: 10px;
  margin-left: 25%;
  margin-right: 25%;
  background-color: rgb(17, 17, 17);
  box-shadow: 5px 5px 5px 5px rgb(245, 235, 235);
  color: #fff;
}
button {
  background-color: rgb(0, 84, 209);
  color: whitesmoke;
  border-radius: 10px;
  padding-top: 10px;
  padding-bottom: 10px;
  display: inline-block;
}

input {
  text-align: center;
}
</style>
