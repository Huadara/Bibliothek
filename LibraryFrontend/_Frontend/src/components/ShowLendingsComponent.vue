<template>
  <div v-bind:style="{ color: 'white' }">
    <h1>Current Lendings</h1>
    <ul>
      <!-- eslint-disable-next-line -->
      <li v-for="lending in lendings">
        {{createReadableString(lending)}}
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: 'ShowLendingsComponent',
  methods: {
    createReadableString (lending) {
      return lending.customer_name + ' ' +
        lending.book_title + ' ' +
        lending.start_date
    }
  },
  props: {
    lendings: ''
  },
  watch: {
    lendings: function (data) {
      const self = this
      self.lendings = data
    }
  }
}
</script>

<style scoped>

</style>
