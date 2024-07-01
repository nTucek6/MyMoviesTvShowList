<script setup lang="ts">
import { ref } from 'vue';


const email = ref('')
const password = ref('')


const formSubmit = () =>{

}

</script>

<template>
  <form @submit.prevent="formSubmit">
    <div class="form-group">
      <label for="email">Email</label>
      <input type="text" name="email" v-model="email" />
    </div>
    <div class="form-group">
      <label for="password">Password</label>
      <input type="password" name="password" v-model="password" />
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

.form-group{
  padding: 10px 0;
}

.form-group:first-of-type{
  padding-top: 0;
}

</style>
