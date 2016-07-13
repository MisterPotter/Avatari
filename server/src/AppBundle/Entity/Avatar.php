<?php
// src/Appbundle/Entity/Avatar.php
namespace AppBundle\Entity;

use Doctrine\ORM\Mapping as ORM;

/**
* @ORM\Entity
* @ORM\Table(name="avatar")
*/
class Avatar
{
    /**
    * @ORM\Column(type="integer")
     * @ORM\Id
     * @ORM\GeneratedValue(strategy="AUTO")
     */
    private $id;

    /**
     * @ORM\Column(type="string", length=100)
     */
    private $name;

    /**
     * @ORM\OneToOne(targetEntity="Account", inversedBy="avatar")
     */
     private $account;

     /**
      * @ORM\OneToMany(targetEntity="Goal", mappedBy="avatar")
      */
      private $goals;

      /**
       * @ORM\ManyToMany(targetEntity="Item", mappedBy="avatars")
       */
       private $items;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $level;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $exp_max;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $exp_current;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $health_max;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $health_current;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $agility_base;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $defence_base;

       /**
        * @ORM\Column(type="integer", length=100)
        */
       private $strength_base;



    /**
     * Get id
     *
     * @return integer
     */
    public function getId()
    {
        return $this->id;
    }

    /**
     * Set name
     *
     * @param string $name
     *
     * @return Avatar
     */
    public function setName($name)
    {
        $this->name = $name;

        return $this;
    }

    /**
     * Get name
     *
     * @return string
     */
    public function getName()
    {
        return $this->name;
    }
    /**
     * Constructor
     */
    public function __construct()
    {
        $this->goals = new \Doctrine\Common\Collections\ArrayCollection();
        $this->items = new \Doctrine\Common\Collections\ArrayCollection();
        $this->level = 1;
        $this->exp_max = 100;
        $this->exp_current = 0;
        $this->health_max = 100;
        $this->health_current = 100;
        $this->agility_base = 5;
        $this->defence_base = 5;
        $this->strength_base = 5;
    }

    /**
     * Set account
     *
     * @param \AppBundle\Entity\Account $account
     *
     * @return Avatar
     */
    public function setAccount(\AppBundle\Entity\Account $account = null)
    {
        $this->account = $account;

        return $this;
    }

    /**
     * Get account
     *
     * @return \AppBundle\Entity\Account
     */
    public function getAccount()
    {
        return $this->account;
    }

    /**
     * Add goal
     *
     * @param \AppBundle\Entity\Goal $goal
     *
     * @return Avatar
     */
    public function addGoal(\AppBundle\Entity\Goal $goal)
    {
        $this->goals[] = $goal;

        return $this;
    }

    /**
     * Remove goal
     *
     * @param \AppBundle\Entity\Goal $goal
     */
    public function removeGoal(\AppBundle\Entity\Goal $goal)
    {
        $this->goals->removeElement($goal);
    }

    /**
     * Get goals
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getGoals()
    {
        return $this->goals;
    }

    /**
     * Add item
     *
     * @param \AppBundle\Entity\Item $item
     *
     * @return Avatar
     */
    public function addItem(\AppBundle\Entity\Item $item)
    {
        $this->items[] = $item;

        return $this;
    }

    /**
     * Remove item
     *
     * @param \AppBundle\Entity\Item $item
     */
    public function removeItem(\AppBundle\Entity\Item $item)
    {
        $this->items->removeElement($item);
    }

    /**
     * Get items
     *
     * @return \Doctrine\Common\Collections\Collection
     */
    public function getItems()
    {
        return $this->items;
    }

    /**
     * Set level
     *
     * @param \int $level
     *
     * @return Avatar
     */
    public function setLevel(\int $level)
    {
        $this->level = $level;

        return $this;
    }

    /**
     * Get level
     *
     * @return \int
     */
    public function getLevel()
    {
        return $this->level;
    }

    /**
     * Set expMax
     *
     * @param \int $expMax
     *
     * @return Avatar
     */
    public function setExpMax(\int $expMax)
    {
        $this->exp_max = $expMax;

        return $this;
    }

    /**
     * Get expMax
     *
     * @return \int
     */
    public function getExpMax()
    {
        return $this->exp_max;
    }

    /**
     * Set expCurrent
     *
     * @param \int $expCurrent
     *
     * @return Avatar
     */
    public function setExpCurrent(\int $expCurrent)
    {
        $this->exp_current = $expCurrent;

        return $this;
    }

    /**
     * Get expCurrent
     *
     * @return \int
     */
    public function getExpCurrent()
    {
        return $this->exp_current;
    }

    /**
     * Set healthMax
     *
     * @param \int $healthMax
     *
     * @return Avatar
     */
    public function setHealthMax(\int $healthMax)
    {
        $this->health_max = $healthMax;

        return $this;
    }

    /**
     * Get healthMax
     *
     * @return \int
     */
    public function getHealthMax()
    {
        return $this->health_max;
    }

    /**
     * Set healthCurrent
     *
     * @param \int $healthCurrent
     *
     * @return Avatar
     */
    public function setHealthCurrent(\int $healthCurrent)
    {
        $this->health_current = $healthCurrent;

        return $this;
    }

    /**
     * Get healthCurrent
     *
     * @return \int
     */
    public function getHealthCurrent()
    {
        return $this->health_current;
    }

    /**
     * Set agilityBase
     *
     * @param integer $agilityBase
     *
     * @return Avatar
     */
    public function setAgilityBase($agilityBase)
    {
        $this->agility_base = $agilityBase;

        return $this;
    }

    /**
     * Get agilityBase
     *
     * @return integer
     */
    public function getAgilityBase()
    {
        return $this->agility_base;
    }

    /**
     * Set defenceBase
     *
     * @param integer $defenceBase
     *
     * @return Avatar
     */
    public function setDefenceBase($defenceBase)
    {
        $this->defence_base = $defenceBase;

        return $this;
    }

    /**
     * Get defenceBase
     *
     * @return integer
     */
    public function getDefenceBase()
    {
        return $this->defence_base;
    }

    /**
     * Set strengthBase
     *
     * @param integer $strengthBase
     *
     * @return Avatar
     */
    public function setStrengthBase($strengthBase)
    {
        $this->strength_base = $strengthBase;

        return $this;
    }

    /**
     * Get strengthBase
     *
     * @return integer
     */
    public function getStrengthBase()
    {
        return $this->strength_base;
    }

}
