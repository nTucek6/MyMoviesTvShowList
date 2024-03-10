<script setup lang="ts">
defineProps({
  message: String,
})
</script>


<template>
<div id="error-message">
{{ message }}
</div>
</template>

<style scoped>

#error-message{
    padding: 15px;
    color: red;
    margin-bottom: 15px;
}

</style>

