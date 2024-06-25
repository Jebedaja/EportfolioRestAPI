<template>
  <div class="home-container">
    <div class="welcome-box">
      <h1>Welcome to E-Portfolio</h1>
      <div class="links">
        <router-link to="/login" class="link-button">Login</router-link>
        <router-link to="/register" class="link-button">Register</router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HomeView'
};
</script>

<style scoped>
.home-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #e0e0e0; /* Zmienione na jasno szary */
}

.welcome-box {
  text-align: center;
  padding: 20px;
  background: white;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
}

h1 {
  font-size: 2.5em;
  margin-bottom: 20px;
  color: #333;
}

.links {
  display: flex;
  justify-content: center;
  gap: 15px;
}

.link-button {
  display: inline-block;
  padding: 10px 20px;
  color: white;
  background-color: #b6b6b6;
  border: none;
  border-radius: 5px;
  text-decoration: none;
  font-size: 1em;
  transition: background-color 0.3s ease;
}

.link-button:hover {
  background-color: #3b3c3c;
}
</style>
