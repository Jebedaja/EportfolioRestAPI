<template>
  <div class="portfolio-container">
    <div class="portfolio-box">
      <h2>Portfolio</h2>
      <router-link :to="{ name: 'add-portfolio' }">
        <button class="add-button">Add New Portfolio</button>
      </router-link>
      <div v-if="portfolios.length > 0">
        <div v-for="portfolio in portfolios" :key="portfolio.id" class="portfolio-item">
          <h3>{{ portfolio.portfolioName }}</h3>
          <p>{{ portfolio.description }}</p>
          <a :href="portfolio.youTubeLink" target="_blank">Watch on YouTube</a>
          <button @click="deletePortfolio(portfolio.id)" class="delete-button">Delete</button>
          <router-link :to="{ name: 'edit-portfolio', params: { id: portfolio.id }}" class="edit-button">Edit</router-link>
        </div>
      </div>
      <div v-else>
        <p>No portfolio content available.</p>
      </div>
      <div v-if="error" class="error-message">
        <p>{{ error }}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      portfolios: [],
      error: null
    };
  },
  mounted() {
    this.fetchPortfolioContent();
  },
  methods: {
    async fetchPortfolioContent() {
      try {
        const response = await fetch('/api/Account/Portfolio', {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
        if (response.ok) {
          this.portfolios = await response.json();
        } else {
          const errorText = await response.text();
          this.error = `Failed to fetch portfolio content: ${response.status} ${errorText}`;
          console.error('Failed to fetch portfolio content', response.status, errorText);
        }
      } catch (error) {
        this.error = `Error fetching portfolio content: ${error.message}`;
        console.error('Error fetching portfolio content:', error);
      }
    },
    async deletePortfolio(id) {
      try {
        const response = await fetch(`/api/Account/Portfolio/${id}`, {
          method: 'DELETE',
          headers: {
            Authorization: `Bearer ${localStorage.getItem('token')}`
          }
        });
        if (response.ok) {
          this.portfolios = this.portfolios.filter(p => p.id !== id);
        } else {
          const errorText = await response.text();
          this.error = `Failed to delete portfolio with id ${id}: ${response.status} ${errorText}`;
          console.error(`Failed to delete portfolio with id ${id}`, response.status, errorText);
        }
      } catch (error) {
        this.error = `Error deleting portfolio with id ${id}: ${error.message}`;
        console.error(`Error deleting portfolio with id ${id}:`, error);
      }
    }
  }
};
</script>

<style scoped>
.portfolio-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  background-color: #e0e0e0;
}

.portfolio-box {
  text-align: center;
  padding: 20px;
  background: white;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  border-radius: 8px;
  width: 80%;
  max-width: 800px;
}

h2 {
  margin-bottom: 20px;
  color: #333;
}

.content {
  text-align: left;
}

.portfolio-item {
  margin-bottom: 20px;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 8px;
}

.portfolio-item h3 {
  margin-bottom: 10px;
  color: #555;
}

.portfolio-item p {
  margin-bottom: 10px;
  color: #777;
}

.portfolio-item a {
  color: #1e90ff;
  text-decoration: none;
  display: block;
  margin-bottom: 5px;
}

.add-button,
.delete-button,
.edit-button {
  background-color: #b6b6b6;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  margin-top: 10px;
  cursor: pointer;
}

.add-button:hover,
.delete-button:hover,
.edit-button:hover {
  background-color: #3b3c3c;
}

.error-message {
  color: red;
  margin-top: 20px;
}
</style>
