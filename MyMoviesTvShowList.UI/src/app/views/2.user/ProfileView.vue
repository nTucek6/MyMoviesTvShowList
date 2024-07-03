<script setup lang="ts">
import { useRoute } from 'vue-router'

const route = useRoute()

const username = route.params.username

const calculateGraphSize = (list:number, totalEntries:number, containerWidth:number) =>{
 return (list / totalEntries) * containerWidth
}


const watched = calculateGraphSize(53,756,380).toFixed(1)


</script>

<template>
  <div>
    {{ watched }}
    <div id="status-graph">
      <span id="watched-graph" :style="{'width' : watched+'px'}"></span>
    </div>
  </div>
</template>

<style scoped lang="scss">

#status-graph{
  width:380px;
  background-color: red;
  height: 10px;
  display: block;
  overflow: hidden;
}

#watched-graph{
 height: 100%;
 display: inline-block;
 background-color: green;
}

</style>
