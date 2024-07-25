<script setup lang="ts">
import { ref, computed, onUnmounted, onMounted, watch, nextTick } from 'vue'
import type { Select } from '../models/select.model'
import '@/assets/custom/select.css'

const props = defineProps({
  StatusOptions: Array<Select>,

  Title: String,
  Synopsis: String,
  ImageData: String,
  Genres: Array<Select>,

  InitialStatus: {
    type: Object
  }
})

const emit = defineEmits(['selectedStatus'])

const showList = ref(false)

const selectedStatus = ref()

const statusDropdown = ref()

const InitialSetupDone = ref<boolean>(false)

const selectedIndex = ref<number>(0)

const items = ['Cast', 'Crew']

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

const selectItem = (index:number) =>{
  selectedIndex.value = index
}

</script>

<template>
  <section>
    <div id="left-side">
      <div id="media-img">
        <img :src="`data:image/jpeg;base64,${ImageData}`" />
      </div>
      <div id="media-info">
        <div id="genres">
          <h4>Genres:</h4>
          <span v-for="g in Genres" :key="g.value">{{ g.label }}</span>
        </div>

      </div>
    </div>

    <div id="center-side">
      <div id="synopsis">
        <h4>Synopsis</h4>
        <p>
          {{ Synopsis }}
        </p>
      </div>
      <div id="tab-content">
        <ul>
          <li
            v-for="(item, index) in items"
            :key="index"
            :class="{ selected: selectedIndex === index }"
            @click="selectItem(index)">
            {{ item }}
          </li>
        </ul>
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

#media-info > #genres{
  display: flex;
  align-items: center;
}

#media-info > #genres > h4::after{
  content: ' ';
}

#media-info > #genres > span{
  font-size: 700;
  line-height: 1.5rem;
  cursor: pointer;
}

#media-info > #genres > span::after{
  content: ', ';
}

#media-info > #genres > span:last-child::after{
  content: none;
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

#tab-content {
  margin-top: 20px;
}

#tab-content > ul {
  list-style: none;
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  border-bottom: 2px solid #333;
}

#tab-content > ul > li {
  padding: 5px 20px;
  position: relative;
  cursor: pointer;
}

#tab-content > ul > li::after {
  content: '';
  position: absolute;
  left: 0;
  right: 0;
  bottom: -2px; /* Adjusted to align with the ul border */
  height: 2px; /* Thickness of the bottom border */
  transition: background-color 0.3s ease;
}

#tab-content > ul > li:hover {
  color: green;
}

#tab-content > ul > li:hover::after {
  background-color: green;
}

.selected,
.selected:hover {
  color: red !important;
}

.selected::after,
.selected:hover::after {
  background-color: red !important;
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
