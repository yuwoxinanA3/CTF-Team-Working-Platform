<template>


    <div class="common-layout">
        <el-container>
            <el-aside width="30%">左侧</el-aside>
            <el-main style="background-color: greenyellow;">
                <CreateTeam></CreateTeam>
            </el-main>
            <el-aside width="30%">右侧</el-aside>
        </el-container>
    </div>
</template>

<script setup lang="ts">
import CreateTeam from '@/components/createTeam.vue';


</script>

<style scoped></style>