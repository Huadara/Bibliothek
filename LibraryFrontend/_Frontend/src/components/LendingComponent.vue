<template>
  <div v-bind:style="{ color: 'white' }">
    <h1>Book</h1>
    <b-container>
      <b-row class="p-2">
        <b-col>
          <h4>Action</h4>
        </b-col>
        <b-col>
          <b-form-group>
            <b-form-radio v-model="action" name="some-radios" value="purchase">Purchase</b-form-radio>
            <b-form-radio v-model="action" name="some-radios" value="lending">Lending</b-form-radio>
            <b-form-radio v-model="action" name="some-radios" value="restore">Restore</b-form-radio>
          </b-form-group>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <div>
            <h4>Customer</h4>
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
      <b-row>
        <b-col>
          <div>
            <h4>Book</h4>
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
      <b-row class="p-3">
        <b-col>
          <h4>Store:</h4>
        </b-col>
        <b-col>
          <h5>{{currentStore}}</h5>
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Amount:</h4>
        </b-col>
        <b-col>
          <input v-model="amount">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="lendBook">Submit</b-button>
        </b-col>
      </b-row>
    </b-container>
    <span>{{msg}}</span>
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
      currentStore: '',
      amount: '',
      books: [],
      customers: [],
      filteredBooks: [],
      filteredCustomers: [],
      action: '',
      msg: ''
    }
  },
  methods: {
    // customers
    doResponseCustomers (response) {
      this.customers = response
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
      if (customer.lastname === undefined) return 'Select Customer'
      return customer.firstname + ' ' + customer.lastname
    },
    // books
    doResponseBooks (response) {
      this.books = response
      this.filteredBooks = this.books
    },
    filterBooks () {
      this.filteredBooks = this.books
        .filter(book => book.title.toLowerCase().startsWith(this.searchBookFilter.toLowerCase()))
    },
    selectedBookChanged (book) {
      this.currentBook = book
      const self = this
      const url = 'http://localhost:5000/library/books/GetStoredBook/' +
      this.currentBook.book_id + '/' +
      this.currentStore
      axios.get(url)
        .then(function (response) {
          if (response != null) console.log('response')
          self.msg = response.data.message
        }).catch(function (error) {
          if (error != null) console.log('error')
          self.msg = 'Please check your entries'
        })
    },
    get_name_book (book) {
      if (book.title === undefined) return 'Select Book'
      return book.title
    },
    // lending
    lendBook () {
      const self = this
      const url = self.action === 'purchase'
        ? 'http://localhost:5000/library/books/BuyBook'
        : (self.action === 'lending'
          ? 'http://localhost:5000/library/books/LendBook'
          : (self.action === 'restore'
            ? 'http://localhost:5000/library/books/RestoreBook'
            : ''))
      console.log(url)
      axios.post(url, {
        customer_id: this.currentCustomer.customer_id,
        book_id: this.currentBook.book_id,
        store_id: this.currentStore,
        amount: this.amount
      }).then(function (response) {
        if (response != null) console.log('response')
        console.log(response)
        self.msg = response.data.message
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Please check your entries'
      })
      self.customer_id = ''
      self.book_id = ''
      self.store_id = ''
      self.amount = ''
    }
  },
  props: {
    serverCustomers: '',
    serverBooks: ''
  },
  watch: {
    serverCustomers: function (data) {
      const self = this
      self.doResponseCustomers(data)
    },
    serverBooks: function (data) {
      const self = this
      self.doResponseBooks(data)
      self.currentStore = self.$parent.$parent.$el.getElementsByTagName('span')[0].textContent
    }
  }
}
</script>

<style scoped>
  #whitespace {
    padding-top: 2.6em;
  }
</style>
