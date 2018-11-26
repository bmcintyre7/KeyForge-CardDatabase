package com.keyforge.libraryaccess.LibraryAccessService.specifications

import com.keyforge.libraryaccess.LibraryAccessService.data.*
import org.springframework.data.jpa.domain.Specifications
import kotlin.reflect.jvm.javaField

data class CardQuery(
        val name: String? = null,
        val text: String? = null,
        val types: MutableList<Type>? = null,
        val houses: MutableList<House>? = null,
        val keywords: MutableList<Keyword>? = null,
        val rarities: MutableList<Rarity>? = null,
        val traitsAnd: MutableList<Trait>? = null,
        val traitsOr: MutableList<Trait>? = null,
        val aember: Map<String, String>,
        val power: Map<String, String>,
        val armor: Map<String, String>,
        val artist: String? = null
) {
    fun hasName(name: String?): Specifications<Card>? = name?.let {
        Card::name.likeIgnoreCase("%" + name + "%")
    }

    fun hasText(text: String?): Specifications<Card>? = text?.let {
        Card::text.likeIgnoreCase("%" + text + "%")
    }

    fun hasTypeIn(types: List<Type>?): Specifications<Card>? = types?.let {
        Card::type.`in`(types)
    }

    fun hasTraitIn(traits: List<Trait>?): Specifications<Card>? = traits?.let {
        or ( traits.map( ::hasTrait ))
    }

    fun hasTraits(traits: List<Trait>?): Specifications<Card>? = traits?.let {
        and ( traits.map( ::hasTrait ))
    }

    fun hasTrait(trait: Trait): Specifications<Card>? = trait?.let {
        where { equal(it.join(Card::traits).get(CardTraits::trait), trait)}
    }

    fun hasRarityIn(rarities: List<Rarity>?): Specifications<Card>? = rarities?.let {
        Card::rarity.`in`(rarities)
    }

    fun hasHouseIn(houses: List<House>?): Specifications<Card>? = houses?.let {
        or ( houses.map( ::hasHouse ) )
    }

    fun hasHouse(house: House?): Specifications<Card>? = house?.let {
        Card::houses.isMember(house)
    }

    fun hasKeywords(keywords: List<Keyword>?): Specifications<Card>? = keywords?.let {
        and ( keywords.map( ::hasKeyword ) )
    }

    fun hasKeyword(keyword: Keyword?): Specifications<Card>? = keyword?.let {
        where { equal(it.join(Card::keywords).get(CardKeywords::keyword), keyword) }
    }

    fun hasAemberGTE(aember: String?): Specifications<Card>? = aember?.let {
        Card::aember.greaterThanOrEqualTo(aember)
    }
    
    fun hasAemberGT(aember: String?): Specifications<Card>? = aember?.let {
        Card::aember.greaterThan(aember)
    }
    
    fun hasAemberLTE(aember: String?): Specifications<Card>? = aember?.let {
        Card::aember.lessThanOrEqualTo(aember)
    }
    
    fun hasAemberLT(aember: String?): Specifications<Card>? = aember?.let {
        Card::aember.lessThan(aember)
    }

    fun hasAemberE(aember: String?): Specifications<Card>? = aember?.let {
        Card::aember.equal(aember)
    }

    fun hasPowerGTE(power: String?): Specifications<Card>? = power?.let {
        Card::power.greaterThanOrEqualTo(power)
    }

    fun hasPowerGT(power: String?): Specifications<Card>? = power?.let {
        Card::power.greaterThan(power)
    }

    fun hasPowerLTE(power: String?): Specifications<Card>? = power?.let {
        Card::power.lessThanOrEqualTo(power)
    }

    fun hasPowerLT(power: String?): Specifications<Card>? = power?.let {
        Card::power.lessThan(power)
    }

    fun hasPowerE(power: String?): Specifications<Card>? = power?.let {
        Card::power.equal(power)
    }

    fun hasArmorGTE(armor: String?): Specifications<Card>? = armor?.let {
        Card::armor.greaterThanOrEqualTo(armor)
    }

    fun hasArmorGT(armor: String?): Specifications<Card>? = armor?.let {
        Card::armor.greaterThan(armor)
    }

    fun hasArmorLTE(armor: String?): Specifications<Card>? = armor?.let {
        Card::armor.lessThanOrEqualTo(armor)
    }

    fun hasArmorLT(armor: String?): Specifications<Card>? = armor?.let {
        Card::armor.lessThan(armor)
    }

    fun hasArmorE(armor: String?): Specifications<Card>? = armor?.let {
        Card::armor.equal(armor)
    }

    fun hasArtist(artist: String?): Specifications<Card>? = artist?.let {
        Card::artist.likeIgnoreCase(artist)
    }

    fun toSpecification() : Specifications<Card> = and(
        hasName(name),
        hasText(text),
        hasTypeIn(types),
        hasKeywords(keywords),
        hasRarityIn(rarities),
        hasHouseIn(houses),
        hasTraitIn(traitsOr),
        hasTraits(traitsAnd),
        hasAemberGTE(aember["GTE"]),
        hasAemberGT(aember["GT"]),
        hasAemberLTE(aember["LTE"]),
        hasAemberLT(aember["LT"]),
        hasAemberE(aember["E"]),
        hasPowerGTE(power["GTE"]),
        hasPowerGT(power["GT"]),
        hasPowerLTE(power["LTE"]),
        hasPowerLT(power["LT"]),
        hasPowerE(power["E"]),
        hasArmorGTE(armor["GTE"]),
        hasArmorGT(armor["GT"]),
        hasArmorLTE(armor["LTE"]),
        hasArmorLT(armor["LT"]),
        hasArmorE(armor["E"]),
        hasArtist(artist)
    )
}

//fun hasHouseIn(houses: MutableList<String>?): Specifications<Card>? = houses?.let {
//    or ( houses.map { hasHouse(it) })
//}
//
//fun hasHouse(house: String?): Specifications<Card>? = house?.let {
//    Card::house.equal(Rarity(name = it))
//}


//fun hasKeywords(keywords: MutableList<String>?): Specifications<Card>? = keywords?.let {
//    and ( keywords.map { hasKeyword(it) })
//}
//
//fun hasKeyword(keyword: String?): Specifications<Card>? = keyword?.let {
//    Card::type.name.
//}

