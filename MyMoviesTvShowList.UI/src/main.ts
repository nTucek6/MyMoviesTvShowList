import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import axios from 'axios'

import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

const app = createApp(App)

app.use(createPinia())
app.use(router)

app.component('font-awesome-icon', FontAwesomeIcon)



axios.defaults.baseURL = "https://localhost:7227/api/"
//axios.defaults.baseURL = "https://26.52.32.10:7169/api/"

app.mount('#app')