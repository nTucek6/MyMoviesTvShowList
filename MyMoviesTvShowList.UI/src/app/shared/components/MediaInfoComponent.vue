<script setup lang="ts">
import { ref, computed, onUnmounted, onMounted, watch, nextTick } from 'vue'
import type { Select } from '../models/select.model'
import '@/assets/custom/select.css'

const props = defineProps({
  StatusOptions: Array<Select>,

  Title: String,
  Synopsis: String,
  ImageData: String,

  InitialStatus: {
    type: Object
  }
})

const emit = defineEmits(['selectedStatus'])

const showList = ref(false)

const selectedStatus = ref()

const statusDropdown = ref()

const InitialSetupDone = ref<boolean>(false)

const displaySelectedStatus = computed(() => {
  return selectedStatus.value || { label: 'Add to list' }
})

const handleClickOutside = (event: any) => {
  if (
    statusDropdown.value &&
    !statusDropdown.value.contains(event.target) &&
    showList.value == true
  ) {
    showList.value = false
  }
}

onMounted(async () => {
  document.addEventListener('click', handleClickOutside)
  selectedStatus.value = props.InitialStatus

  await nextTick()
  InitialSetupDone.value = true
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
})

const toggleList = () => {
  showList.value = !showList.value
}

watch(selectedStatus, () => {
  if (InitialSetupDone.value) {
    toggleList()
    emit('selectedStatus', selectedStatus.value)
  }
})
</script>

<template>
  <section>
    <div id="left-side">
      <div id="media-img">
        <img :src="`data:image/jpeg;base64,${ImageData}`" />
      </div>
    </div>

    <div id="center-side">
      <div id="synopsis">
        <h4>Synopsis</h4>
        <p>
          {{ Synopsis }}
        </p>
      </div>
    </div>

    <div id="right-side">
      <ul id="list-menu">
        <li id="watched-select" ref="statusDropdown">
          <div class="custom-select" :class="showList ? 'active' : null">
            <button class="select-button" @click="toggleList">
              <span class="selected-value">{{ displaySelectedStatus.label }}</span
              ><span class="arrow"></span>
            </button>
            <ul class="select-dropdown" role="listbox" id="select-dropdown">
              <li
                role="option"
                v-for="(i, index) in StatusOptions"
                :key="index"
                @click="toggleList"
              >
                <input type="radio" :value="i" :id="i.label" v-model="selectedStatus" />
                <label :for="i.label">{{ i.label }}</label>
              </li>
            </ul>
          </div>
        </li>
        <li>Rate</li>
        <li></li>
        <li></li>
      </ul>
    </div>
  </section>
</template>

<style lang="scss">
section {
  display: flex;
}

#left-side {
  width: 25%;
  height: 100%;
  padding: 3px;
}

#left-side > #media-img > img {
  width: 100%;
}

/* ---------------------------------------------------------------------- */

#center-side {
  width: 45%;
  height: 100%;
  padding: 0px 10px;
}

#center-side > #synopsis > h4 {
  border-bottom: 1px solid rgb(0, 0, 0);
  margin-bottom: 4px;
  padding-bottom: 2px;
  font-size: 20px;
}

/* ---------------------------------------------------------------------- */

#right-side {
  width: 30%;
  height: 100%;
}

#right-side > #list-menu {
  list-style: none;
  background-color: rgb(173, 196, 214);
}

#right-side > #list-menu > li {
  padding: 10px 0;
  border-bottom: 1px solid rgb(122, 113, 100);
}

#right-side > #list-menu > li:last-child {
  border-bottom: none;
}

#watched-select {
  display: flex;
  justify-content: center;
}
</style>
