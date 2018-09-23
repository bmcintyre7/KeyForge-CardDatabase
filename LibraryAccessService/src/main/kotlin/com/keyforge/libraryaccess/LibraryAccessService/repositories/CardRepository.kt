package com.keyforge.libraryaccess.LibraryAccessService.repositories


import com.keyforge.libraryaccess.LibraryAccessService.data.Card
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface CardRepository : JpaRepository<Card, Int>