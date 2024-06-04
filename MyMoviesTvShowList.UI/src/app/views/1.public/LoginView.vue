<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useAuthentication } from '@/stores/authentication'
import  UserLoginDTO from '@/app/shared/models/user-login.model'
import ErrorComponent from '@/app/shared/components/ErrorComponent.vue'

const auth = useAuthentication()

const User = ref<UserLoginDTO>(new UserLoginDTO())

const errorData = computed(() => auth.errorData)

const hasError = ref<boolean>(false)

const signInButtonPressed = async () => {
  await auth.Login(User.value)
}

watch(errorData, () => {
  hasError.value = errorData.value.StatusCode > 0
})
</script>

<template>
  <section id="login-form">
    <div id="title">
      <h1>Login</h1>
    </div>

    <ErrorComponent v-if="hasError" :message="errorData.Message" />

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

<style scoped lang="scss">

$text_color : #fff;

#login-form {
  border: 1px #fff;
  border-radius: 10px;
  text-align: center;
  padding: 10px;
  margin-left: 25%;
  margin-right: 25%;
  background-color: rgb(17, 17, 17);
  box-shadow: 5px 5px 5px 5px rgb(245, 235, 235);
}
#login-form #title {
  margin-bottom: 15px;
  padding-bottom: 15px;
  border-bottom: 1px solid #fff;
}

#login-form #title h1 {
  color: $text_color ;
}

.forgotPassword {
  text-decoration: none;
  color: $text_color ;
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
  color: black;
}

</style>
