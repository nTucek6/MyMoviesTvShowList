<script setup lang="ts">
import {ref,computed} from 'vue'
import { useAuthentication } from '@/stores/Authentication/authentication';

const Email = ref("")
const Username = ref("")
const Password = ref("")
const PasswordConfirm = ref("")

const signInButtonPressed = async () =>
{
  await useAuthentication().Register(Email.value,Username.value,Password.value)
}


</script>

<template>
<main>
 <h1>Register</h1>
    <hr class="mt-5 mb-5" />
 <form @submit.prevent="signInButtonPressed">
  <div class="form-group mb-3">
   <input type="email" v-model="Email" class="w-50" id="email" placeholder="Email" required />
  </div>
  <div class="form-group mb-3">
   <input type="text" v-model="Username" class="w-50" id="username" placeholder="Username" required />
  </div>
  <div class="form-group mb-3">
   <input type="password" v-model="Password" class="w-50" id="password" placeholder="Password" required />
  </div>
  <div class="form-group mb-3">
   <input type="password" v-model="PasswordConfirm" class="w-50" id="passwordRepeat" placeholder="Confirm Password" required />
  </div>

  <button type="submit" class="btn w-25">Register</button>
</form>
</main>
</template>

<style scoped>
main
{
    border: 1px black solid;
    border-radius: 10px;
    text-align: center;
    padding: 10px;
    margin-left: 25%;
    margin-right: 25%;
    box-shadow: 5px 5px 5px 5px grey;
}
button
{
    background-color: rgb(0, 84, 209);
    color: whitesmoke;
    border-radius: 10px;  
    padding-top: 10px;
    padding-bottom: 10px;
    display: inline-block;  
}

input
{
    text-align: center;
}

</style>