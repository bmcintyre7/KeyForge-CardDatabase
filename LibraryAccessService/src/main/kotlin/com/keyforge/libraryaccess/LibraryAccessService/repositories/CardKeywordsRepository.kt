package com.keyforge.libraryaccess.LibraryAccessService.repositories

import com.keyforge.libraryaccess.LibraryAccessService.data.CardExpansions
import com.keyforge.libraryaccess.LibraryAccessService.data.CardHouses
import com.keyforge.libraryaccess.LibraryAccessService.data.Type
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.stereotype.Repository

@Repository
interface CardHousesRepository : JpaRepository<CardHouses, Int> {
    fun findByCardId(id: Int) : CardHouses
    fun findByHouseId(id: Int) : CardHouses
}