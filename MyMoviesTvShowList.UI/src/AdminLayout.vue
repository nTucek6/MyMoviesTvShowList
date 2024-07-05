<script setup lang="ts">
import { RouterView, useRoute } from 'vue-router'
import { ref, watch, onMounted } from 'vue'
import AdminHeader from './app/shared/components/admin/header/AdminHeader.vue'
import { useAuthentication } from './stores/admin/authentication'

const authentication = useAuthentication()

const route = useRoute()

const page = ref()

onMounted(() => {
  authentication.CheckAdminLogin()
  page.value = route.name
})

watch(route, () => {
  page.value = route.name
})
</script>

<template>
  <main>
    <AdminHeader />
    <div id="container">
      <div id="page">
        <span>{{ page }}</span>
      </div>
      <div id="content">
        <RouterView />
      </div>
    </div>
  </main>
</template>

<style scoped lang="scss">
#container {
  margin-left: 15%;
  //padding:25px;
}

#page {
  background-color: aqua;
  height: 50px;
  display: flex;
  align-items: center;
}

#page > span {
  font-size: 20px;
  padding-left: 10px;
}

#content {
  padding: 5px 10px 0;
}
</style>
