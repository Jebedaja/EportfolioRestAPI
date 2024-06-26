import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/components/HomeView.vue';
import LoginView from '@/components/LoginView.vue';
import RegisterView from '@/components/RegisterView.vue';
import PortfolioView from '@/components/PortfolioView.vue';
import AddPortfolio from '@/components/AddPortfolio.vue';
import EditPortfolio from '@/components/EditPortfolio.vue';

const requireAuth = (to, from, next) => {
  const token = localStorage.getItem('token');
  console.log('Checking auth, token:', token);
  if (!token) {
    next({ name: 'login' });
  } else {
    next();
  }
};


const routes = [
  { path: '/', name: 'HomeView', component: HomeView },
  { path: '/login', name: 'login', component: LoginView },
  { path: '/register', name: 'register', component: RegisterView },
  { 
    path: '/portfolio', 
    name: 'portfolio', 
    component: PortfolioView,
    beforeEnter: requireAuth
  },
  { 
    path: '/add-portfolio', 
    name: 'add-portfolio', 
    component: AddPortfolio,
    beforeEnter: requireAuth
  },
  { 
    path: '/edit-portfolio/:id', 
    name: 'edit-portfolio', 
    component: EditPortfolio, 
    props: true,
    beforeEnter: requireAuth
  }
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
});

export default router;
