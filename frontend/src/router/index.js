import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '@/components/HomeView.vue';
import LoginView from '@/components/LoginView.vue';
import RegisterView from '@/components/RegisterView.vue';
import PortfolioView from '@/components/PortfolioView.vue';
import AddPortfolio from '@/components/AddPortfolio.vue';
import EditPortfolio from '@/components/EditPortfolio.vue';

// Middleware do sprawdzania, czy użytkownik jest zalogowany
const requireAuth = (to, from, next) => {
  const token = localStorage.getItem('token'); // Pobiera token z localStorage
  console.log('Checking auth, token:', token); // Loguje token do konsoli
  if (!token) { // Jeśli token nie istnieje, przekierowuje użytkownika na stronę logowania
    next({ name: 'login' });
  } else { // Jeśli token istnieje, pozwala na przejście do docelowej strony
    next();
  }
};

// Definicje tras aplikacji
const routes = [
  { path: '/', name: 'HomeView', component: HomeView }, // Strona główna
  { path: '/login', name: 'login', component: LoginView }, // Strona logowania
  { path: '/register', name: 'register', component: RegisterView }, // Strona rejestracji
  { 
    path: '/portfolio', 
    name: 'portfolio', 
    component: PortfolioView,
    beforeEnter: requireAuth // Sprawdza, czy użytkownik jest zalogowany przed wejściem na stronę
  },
  { 
    path: '/add-portfolio', 
    name: 'add-portfolio', 
    component: AddPortfolio,
    beforeEnter: requireAuth // Sprawdza, czy użytkownik jest zalogowany przed wejściem na stronę
  },
  { 
    path: '/edit-portfolio/:id', 
    name: 'edit-portfolio', 
    component: EditPortfolio, 
    props: true, // Przekazuje parametry trasy jako propsy do komponentu
    beforeEnter: requireAuth // Sprawdza, czy użytkownik jest zalogowany przed wejściem na stronę
  }
];

// Tworzy instancję routera
const router = createRouter({
  history: createWebHistory(process.env.BASE_URL), // Używa trybu historii przeglądarki
  routes // Przekazuje definicje tras do routera
});

export default router;
