<template>
  <div>
    <h1>Lend Book</h1>
    <b-container>
      <b-row>
        <b-col>
          <div>
            <h3>Customer</h3>
            <input class="form-control" placeholder="Search Customer Lastname" v-model="searchCustomerFilter" @keyup="filterCustomers">
          </div>
        </b-col>
        <b-col>
          <b-dropdown v-bind:text="get_name_cust(currentCustomer)" id="whitespace">
            <b-dropdown-item v-for="customer in filteredCustomers" :key="customer.id"
                             @click="selectedCustomerChanged(customer)">
              {{get_name_cust(customer)}}
            </b-dropdown-item>
          </b-dropdown>
        </b-col>
      </b-row>
      <!-- IIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII -->
      <b-row>
        <b-col>
          <div>
            <h3>Book</h3>
            <input class="form-control" placeholder="Search Book Title" v-model="searchBookFilter" @keyup="filterBooks">
          </div>
        </b-col>
        <b-col>
          <b-dropdown v-bind:text="get_name_book(currentBook)" id="whitespace">
            <b-dropdown-item v-for="book in filteredBooks" :key="book.id"
                             @click="selectedBookChanged(book)">
              {{get_name_book(book)}}
            </b-dropdown-item>
          </b-dropdown>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="lendBook">Lend Book</b-button>
        </b-col>
      </b-row>
    </b-container>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'LendingComponent',
  data () {
    return {
      searchCustomerFilter: '',
      searchBookFilter: '',
      currentBook: Object,
      currentCustomer: Object,
      books: [],
      customers: [],
      filteredBooks: [],
      filteredCustomers: []
    }
  },
  methods: {
    // customers
    doResponseCustomers (response) {
      this.customers = response.data
      this.filteredCustomers = this.customers
    },
    filterCustomers () {
      this.filteredCustomers = this.customers
        .filter(cust => cust.lastname.toLowerCase().startsWith(this.searchCustomerFilter.toLowerCase()))
    },
    selectedCustomerChanged (customer) {
      this.currentCustomer = customer
    },
    get_name_cust (customer) {
      if (customer.firstname === undefined) return 'Select Customer'
      return customer.firstname + ' ' + customer.lastname
    },
    // books
    doResponseBooks (response) {
      this.books = response.data
      this.filteredBooks = this.books
    },
    filterBooks () {
      this.filteredBooks = this.books
        .filter(book => book.title.toLowerCase().startsWith(this.searchBookFilter.toLowerCase()))
    },
    selectedBookChanged (book) {
      this.currentBook = book
    },
    get_name_book (book) {
      if (book.title === undefined) return 'Select Book'
      return book.title
    },
    // lending
    lendBook () {
      /*
      axios.post('http://localhost:5000/library/books', {
        title: this.title,
        author: this.author,
        publisher: this.publisher,
        isbn: this.isbn,
        price: this.price
      }).then(function (response) {
        if (response != null) console.log('response')
        self.msg = 'Add successful'
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Please check your entries'
      })
      */
    }
  },
  created () {
    const self = this
    axios.get('http://localhost:5000/library/customers')
      .then(function (customersResponse) {
        self.doResponseCustomers(customersResponse)
        axios.get('http://localhost:5000/library/books')
          .then(function (booksResponse) {
            self.doResponseBooks(booksResponse)
          })
      })
  }
}
</script>

<style scoped>
  #whitespace {
    padding-top: 2.6em;
  }
</style>
