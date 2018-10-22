package com.keyforge.libraryaccess.LibraryAccessService.data

import com.fasterxml.jackson.annotation.JsonIgnore
import org.hibernate.annotations.BatchSize
import javax.persistence.*

@Entity
@Table(name = "cardHouses")
data class CardHouses (
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    val id: Int? = null,
    @JsonIgnore
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "cardId")
    @BatchSize(size=50)
    val card: Card,
    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "houseId")
    @BatchSize(size=50)
    val house: House
)