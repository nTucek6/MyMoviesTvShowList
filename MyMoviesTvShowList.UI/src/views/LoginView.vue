<script setup lang="ts">
import {ref} from 'vue'

const Email = ref(null)
const Password = ref(null)

const signInButtonPressed = async () =>
{
  console.log(Email.value);
}



</script>

<template>
<main>
 <h1>Login</h1>
    <hr class="mt-5 mb-5" />
 <form @submit.prevent="signInButtonPressed">
  <div class="form-group  mb-3">
   <input type="text" v-model="Email" class="w-50" id="email" placeholder="Email" />
  </div>
  <div class="form-group mb-3">
   <input type="password" v-model="Password" class="w-50" id="password" placeholder="Password" />
  </div>

  <div class="mb-5">
    <RouterLink class="forgotPassword"  to=""><small>Forgot password?</small></RouterLink>
  </div>
 
  
  <button type="submit" class="btn w-25">Login</button>
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

.forgotPassword
{
    text-decoration: none;
    color: black;
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