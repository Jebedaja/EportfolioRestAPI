<template>
    <div class="edit-portfolio">
      <h2>Edit Portfolio</h2>
      <form @submit.prevent="updatePortfolio">
        <div class="form-group">
          <label for="portfolioName">Portfolio Name:</label>
          <input type="text" id="portfolioName" v-model="portfolio.portfolioName" required>
        </div>
        <div class="form-group">
          <label for="description">Description:</label>
          <textarea id="description" v-model="portfolio.description" required></textarea>
        </div>
        <div class="form-group">
          <label for="youTubeLink">YouTube Link:</label>
          <input type="text" id="youTubeLink" v-model="portfolio.youTubeLink" required>
        </div>
        <button type="submit">Update Portfolio</button>
      </form>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        portfolio: {
          id: null,
          portfolioName: '',
          description: '',
          youTubeLink: ''
        }
      };
    },
    async created() {
      const id = this.$route.params.id;
      await this.fetchPortfolio(id);
    },
    methods: {
      async fetchPortfolio(id) {
        try {
          const response = await fetch(`/api/Account/Portfolio/${id}`, {
            headers: {
              Authorization: `Bearer ${localStorage.getItem('token')}`
            }
          });
          if (response.ok) {
            this.portfolio = await response.json();
          } else {
            console.error(`Failed to fetch portfolio with id ${id}`);
          }
        } catch (error) {
          console.error(`Error fetching portfolio with id ${id}:`, error);
        }
      },
      async updatePortfolio() {
        try {
          const response = await fetch(`/api/Account/Portfolio/${this.portfolio.id}`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json',
              Authorization: `Bearer ${localStorage.getItem('token')}`
            },
            body: JSON.stringify(this.portfolio)
          });
          if (response.ok) {
            this.$router.push({ name: 'portfolio' }); // Przekierowanie do widoku portfolio po edycji
          } else {
            console.error(`Failed to update portfolio with id ${this.portfolio.id}`);
          }
        } catch (error) {
          console.error(`Error updating portfolio with id ${this.portfolio.id}:`, error);
        }
      }
    }
  };
  </script>
  
  <style scoped>
  .edit-portfolio {
    text-align: center;
    padding: 20px;
    background: white;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    border-radius: 8px;
    width: 80%;
    max-width: 800px;
    margin: auto;
  }
  
  h2 {
    margin-bottom: 20px;
    color: #333;
  }
  
  .form-group {
    margin-bottom: 20px;
  }
  
  label {
    display: block;
    margin-bottom: 8px;
    color: #555;
  }
  
  input[type="text"],
  textarea {
    width: 100%;
    padding: 8px;
    font-size: 16px;
    border: 1px solid #ccc;
    border-radius: 4px;
  }
  
  button {
    background-color: #b6b6b6;
    color: white;
    border: none;
    padding: 10px 20px;
    border-radius: 4px;
    cursor: pointer;
  }
  
  button:hover {
    background-color: #3b3c3c;
  }
  </style>
  